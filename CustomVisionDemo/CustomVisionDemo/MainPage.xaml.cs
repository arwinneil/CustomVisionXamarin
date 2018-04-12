using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace CustomVisionDemo
{
    public partial class MainPage : ContentPage
    {
        private const string subscriptionKey = "dffd51fdbd3f464da21433e7abc36ae2";

        private const string uriBase = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Prediction/53277e9c-6b11-4be2-91ca-040f6773e344/image?iterationId=b3050ece-2182-4e99-b3d1-418e12fc6553";

        private PredictionResponse results;

        public MainPage()
        {
            InitializeComponent();
        }

        async private void TakePhoto(object sender, EventArgs e)
        {
            InitialiseContent();
          
            Plugin.Media.Abstractions.StoreCameraMediaOptions options = new Plugin.Media.Abstractions.StoreCameraMediaOptions();

            

            var image = await CrossMedia.Current.TakePhotoAsync(options);
            StatusImage.Source = image.Path;
            RequestPrediction(image.Path);
            Status.IsVisible = true;
            StatusLabel.Text = "Analysing...";
            Activity.IsRunning = true;
        }

        public async void RequestPrediction(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Prediction-Key", subscriptionKey);

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uriBase, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                DeserialiseResults(contentString);
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            /// <summary>
            /// Returns the contents of the specified file as a byte array.
            /// </summary>
            /// <param name="imageFilePath">The image file to read.</param>
            /// <returns>The byte array of the image data.</returns>

            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public void DeserialiseResults(string json)
        {
            results = JsonConvert.DeserializeObject<PredictionResponse>(json);
            UpdateScreen();

        }


        void UpdateScreen()
        {
           

            if (results.Predictions[0].Tag == "Strawberry Milk")
                StrawberryFrame.IsVisible = true;
            else if (results.Predictions[0].Tag == "Melon Milk")
                MelonFrame.IsVisible = true;
            Activity.IsRunning = false;

            StatusLabel.Text ="Score : " +results.Predictions[0].Probability.ToString();
        }


        public void InitialiseContent()
        {

            MelonFrame.IsVisible = false;
            StrawberryFrame.IsVisible = false;


        }
    }
}