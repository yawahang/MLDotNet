using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using MLDotNet.Api.Model;

namespace MLDotNet.Api.Controllers
{
    public class MLController : Controller
    {
        private readonly PredictionEnginePool<MvMLInput, MvMLOutput> _engine;
        public MLController(PredictionEnginePool<MvMLInput, MvMLOutput> engine)
        {
            _engine = engine;
        }

        [HttpPost]
        [AllowAnonymous]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Predict([FromForm] IFormFile image)
        {
            try
            {
                long fileSize = image.Length;
                string fileType = image.ContentType;

                if (fileSize > 0)
                {
                    using var stream = new MemoryStream();
                    image.CopyTo(stream);
                    var imageBytes = stream.ToArray();

                    MvMLInput input = new() { Image = imageBytes };

                    MvMLOutput output = _engine.Predict(modelName: "MLModel", example: input);

                    return await Task.FromResult(Ok(output));
                }
                else
                {
                    return await Task.FromResult(Ok("Error"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("BadRequest");
            }
        }
    }
}
