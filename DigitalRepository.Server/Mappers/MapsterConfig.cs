namespace DigitalRepository.Server.Mappers
{
    using Entities.Models;
    using Entities.Response;
    using Entities.Request;
    using Mapster;

    public abstract class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<DocumentRequest, Document>.NewConfig()
                .Map(dest => dest.DocumentNumber, src => Guid.NewGuid().ToString())
                .Map(dest => dest.Author, src => src.Author)
                .Map(dest => dest.UserIp, src => src.CreatedBy)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy)
                .Ignore(dest => dest.UserIp)
                .Ignore(dest => dest.ElaborationDate)
                .Ignore(dest => dest.LoadDate)
                .Ignore(dest => dest.Size)
                .Ignore(dest => dest.State)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt!)
                .Ignore(dest => dest.Path);

            TypeAdapterConfig<Document, DocumentResponse>.NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.Path, src => src.Path)
                .Map(dest => dest.DocumentNumber, src => src.DocumentNumber)
                .Map(dest => dest.State, src => src.State)
                .Map(dest => dest.UserIp, src => src.UserIp)
                .Map(dest => dest.Size, src => src.Size)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.LoadDate, src => src.LoadDate.ToString("dd/MM/yyyy HH:mm:ss"))
                .Map(dest => dest.ElaborationDate, src => src.ElaborationDate.ToString("dd/MM/yyyy HH:mm:ss"))
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"))
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm:ss") : null)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy);


            TypeAdapterConfig<Document, Document>.NewConfig();
        }
    }
}
