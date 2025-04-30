using Content.Goobstation.Common.AiTracker;
using Content.Shared.Follower;
using Content.Shared.Follower.Components;
using Content.Shared.Silicons.StationAi;

namespace Content.Goobstation.Client.StationAi;

public sealed partial class GoobStationAiSystem : EntitySystem
{
    [Dependency] private readonly SharedStationAiSystem _stationAi = default!;
    [Dependency] private readonly FollowerSystem _followerSystem = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);
        var eqe = EntityQueryEnumerator<FollowerComponent, StationAiTrackerComponent>();
        while (eqe.MoveNext(out var uid, out var follower, out _))
        {
            if (_stationAi.InRange(follower.Following, uid, false) == false)
                _followerSystem.StopFollowingEntity(uid, follower.Following);
        }
    }
}
