using DigitalRepository.Interfaces;
using DigitalRepository.Server.Entities.Models;
using DigitalRepository.Server.Entities.Request;
using DigitalRepository.Server.Entities.Response;
using DigitalRepository.Server.Interceptors.Interfaces;
using DigitalRepository.Server.Services.Core;
using FluentValidation.Results;
using Lombok.NET;

namespace DigitalRepository.Server.Interceptors.DocumentInterceptor
{
    [AllArgsConstructor]
    public partial class SaveDocumentServer : IEntityBeforeCreateInterceptor<Document, DocumentRequest>
    {
        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<EntityService<Document, DocumentRequest, long>> _logger;
        private readonly IResources _resources;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="response">The response<see cref="Response{TEntity}"/></param>
        /// <param name="request">The request<see cref="DocumentRequest"/></param>
        /// <returns>The <see cref="Response{Signature, List{ValidationFailure}}"/></returns>
        public Response<Document, List<ValidationFailure>> Execute(Response<Document, List<ValidationFailure>> response, DocumentRequest request)
        {
            try
            {
                Response<string> image = _resources.SaveDocumentInServer(request.File, "");

                if (!image.Success)
                {
                    response.Success = false;
                    response.Message = "Error al guardar documento en el servidor";
                    return response;
                }

                response.Success = true;
                response.Message = "Imagen guardada correctamente";
                response.Data = new Document
                {
                    CreatedBy = request.CreatedBy!.Value,
                    ElaborationDate = DateTime.UtcNow,
                    LoadDate = DateTime.ParseExact(request.ElaborationDate!, "yyyy-MM-dd", null).ToUniversalTime(),
                    Size = request.File!.Length,
                    State = 1,
                    UserId = request.CreatedBy!.Value,
                    UserIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "IP no disponible",
                    UpdatedBy = request.UpdatedBy,
                    Path = image.Data!,
                    DocumentNumber = request.DocumentNumber!,
                    Author = request.Author!
                };

                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error al guardar el documento antes de crear en la bd";

                _logger.LogError(e, "Error al guardar del documento antes de crear en la bd {order}, Usuario: {user} Error: {error}", response.Data?.Id, response.Data?.CreatedBy, e.Message);

                return response;
            }
        }
    }
}
