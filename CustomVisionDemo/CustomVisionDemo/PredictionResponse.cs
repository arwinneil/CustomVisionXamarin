using System;
using System.Collections.Generic;
using System.Text;

namespace CustomVisionDemo
{
  



    public class Region
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Prediction
    {
        public double Probability { get; set; }
        public string TagId { get; set; }
        public string TagName { get; set; }
        public Region Region { get; set; }
    }

    public class PredictionResponse
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public string Created { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}
