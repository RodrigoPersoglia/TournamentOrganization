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

        CreateMap<Match, MatchDto>()
            .ForMember(dest => dest.Player1, opt => opt.MapFrom(src => $"{src.Player1.FirstName} {src.Player1.LastName}"))
            .ForMember(dest => dest.Player2, opt => opt.MapFrom(src => $"{src.Player2.FirstName} {src.Player2.LastName}"))
            .ForMember(dest => dest.Winner, opt => opt.MapFrom(src => $"{src.Winner.FirstName} {src.Winner.LastName}"));

        CreateMap<Player, PlayerDto>().ReverseMap();
    }
}
