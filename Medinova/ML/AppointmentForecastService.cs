using Medinova.Models;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medinova.ML
{
    public class AppointmentData
    {
        public float AppointmentCount { get; set; }
    }

    public class AppointmentPrediction
    {
        public float[] ForecastedAppointments { get; set; }
    }

    public class AppointmentForecastService
    {
        public float[] GetMonthlyForecast()
        {
            using (var context = new MedinovaContext())
            {

                var dailyData = context.Appointments
                     .GroupBy(x => System.Data.Entity.DbFunctions.TruncateTime(x.AppointmentDate))
                     .OrderBy(g => g.Key)
                     .Select(g => new AppointmentData
                     {
                         AppointmentCount = g.Count()
                     })
                     .ToList();

                var mlContext = new MLContext();

                var dataView = mlContext.Data.LoadFromEnumerable(dailyData);

                var pipeline = mlContext.Forecasting.ForecastBySsa(
                  outputColumnName: nameof(AppointmentPrediction.ForecastedAppointments),
                  inputColumnName: nameof(AppointmentData.AppointmentCount),
                  windowSize: 15,
                  seriesLength: dailyData.Count,
                  trainSize: dailyData.Count,
                  horizon: 300);   


                var model = pipeline.Fit(dataView);

                var forecastingEngine =
                    model.CreateTimeSeriesEngine<AppointmentData, AppointmentPrediction>(mlContext);

                var prediction = forecastingEngine.Predict();


                var lastRealDate = context.Appointments.Max(x => x.AppointmentDate);

                var startDate = new DateTime(lastRealDate.Year, lastRealDate.Month, 1)
                    .AddMonths(1);


                var monthlyForecast = prediction.ForecastedAppointments
                     .Select((value, index) => new
                     {
                         Date = startDate.AddDays(index),
                         Value = value
                     })
                     .GroupBy(x => new { x.Date.Year, x.Date.Month })
                     .Select(g => (float)Math.Round(g.Sum(x => x.Value)))
                     .Take(8)
                     .ToArray();

                return monthlyForecast;
            }
        }

    }
}