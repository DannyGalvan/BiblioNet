using DigitalRepository.Server.Services.Interfaces;
using Lombok.NET;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DigitalRepository.Server.Entities.Models;
using DigitalRepository.Server.Entities.Request;
using DigitalRepository.Server.Entities.Response;
using FluentValidation.Results;

namespace DigitalRepository.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllArgsConstructor]
    [Authorize]
    public partial class DocumentController : CommonController
    {
        private readonly IEntityService<Document, DocumentRequest, long> _documentService;
        private readonly IMapper _mapper;

        [HttpGet]
        public ActionResult Get(string? filters, bool thenInclude, int pageNumber = 1, int pageSize = 30)
        {
            var response = _documentService.GetAll(filters, thenInclude, pageNumber, pageSize);

            if (response.Success)
            {
                Response<List<DocumentResponse>> successResponse = new()
                {
                    Data = _mapper.Map<List<Document>, List<DocumentResponse>>(response.Data!),
                    Success = response.Success,
                    Message = response.Message,
                    TotalResults = response.TotalResults,
                };

                return Ok(successResponse);

            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);

        }

        [HttpGet("{id}")]
        public IActionResult GetSignature(long id)
        {
            var response = _documentService.GetById(id);

            if (response.Success)
            {
                Response<DocumentResponse> successResponse = new()
                {
                    Data = _mapper.Map<Document, DocumentResponse>(response.Data!),
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(successResponse);
            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateSignature([FromForm] DocumentRequest signatureRequest)
        {
            signatureRequest.CreatedBy = GetUserId();
            var response = _documentService.Create(signatureRequest);

            if (response.Success)
            {
                Response<DocumentResponse> successResponse = new()
                {
                    Data = _mapper.Map<Document, DocumentResponse>(response.Data!),
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(successResponse);
            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);
        }

        [HttpPut]
        public IActionResult UpdateSignature([FromForm] DocumentRequest signatureRequest)
        {
            signatureRequest.UpdatedBy = GetUserId();
            var response = _documentService.Update(signatureRequest);

            if (response.Success)
            {
                Response<DocumentResponse> successResponse = new()
                {
                    Data = _mapper.Map<Document, DocumentResponse>(response.Data!),
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(successResponse);
            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);
        }

        [HttpPatch]
        public IActionResult PartialUpdateSignature([FromForm] DocumentRequest signatureRequest)
        {
            signatureRequest.UpdatedBy = GetUserId();
            var response = _documentService.PartialUpdate(signatureRequest);

            if (response.Success)
            {
                Response<DocumentResponse> successResponse = new()
                {
                    Data = _mapper.Map<Document, DocumentResponse>(response.Data!),
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(successResponse);
            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSignature(long id)
        {
            var response = _documentService.Delete(id, GetUserId());

            if (response.Success)
            {
                Response<DocumentResponse> successResponse = new()
                {
                    Data = _mapper.Map<Document, DocumentResponse>(response.Data!),
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(successResponse);
            }

            Response<List<ValidationFailure>> errorResponse = new()
            {
                Data = response.Errors,
                Success = response.Success,
                Message = response.Message
            };

            return BadRequest(errorResponse);
        }

    }
}
