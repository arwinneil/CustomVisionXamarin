using System;
using System.Collections.Generic;
using System.Text;

namespace CustomVisionDemo
{
    public class Prediction
    {
        public string TagId { get; set; }
        public string Tag { get; set; }
        public double Probability { get; set; }
    }

    public class PredictionResponse
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}
