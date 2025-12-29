using AutoMapper;

namespace Domain.Common.Mappings
{
    /// <summary>
    /// This Interface Help Us To Map Dto to Entity Or another Dto
    /// when we Use Change One Dto To Another object To pass Function
    /// for Example Send Dto To DomainService Function As Entity Or Some Other Model Object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
