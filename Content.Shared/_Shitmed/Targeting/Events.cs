// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Shitmed.Targeting.Events;

[Serializable, NetSerializable]
public sealed class TargetChangeEvent(NetEntity uid, TargetBodyPart bodyPart) : EntityEventArgs
{
    public NetEntity Uid { get; } = uid;
    public TargetBodyPart BodyPart { get; } = bodyPart;
}

[Serializable, NetSerializable]
public sealed class TargetIntegrityChangeEvent(NetEntity uid, bool refreshUi = true) : EntityEventArgs
{
    public NetEntity Uid { get; } = uid;
    public bool RefreshUi { get; } = refreshUi;
}
