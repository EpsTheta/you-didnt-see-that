using System;
using VentLib.Utilities.Optionals;

namespace TOHTOR.API.Vanilla.Sabotages;

public interface ISabotage
{
    public SabotageType SabotageType();

    public bool Fix(PlayerControl? fixer = null);

    public Optional<PlayerControl> Caller();

    public void Sabotage(PlayerControl sabotageCaller);

    public static ISabotage From(SabotageType type, PlayerControl? caller = null)
    {
        return type switch
        {
            Sabotages.SabotageType.Lights => new ElectricSabotage(caller),
            Sabotages.SabotageType.Communications => new CommsSabotage(caller),
            Sabotages.SabotageType.Oxygen => new OxygenSabotage(caller),
            Sabotages.SabotageType.Reactor => new ReactorSabotage(caller),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}