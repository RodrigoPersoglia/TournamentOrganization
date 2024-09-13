using AutoMapper;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TournamentDto, Tournament>()
            .ForMember(dest => dest.PlayerTournaments, opt => opt.MapFrom(src =>
                src.PlayersId.Select(id => new PlayerTournament { PlayerId = id })));

        CreateMap<Tournament, TournamentDto>()
            .ForMember(dest => dest.PlayersId, opt => opt.MapFrom(src =>
                src.PlayerTournaments.Select(pt => pt.PlayerId)));
    }
}
