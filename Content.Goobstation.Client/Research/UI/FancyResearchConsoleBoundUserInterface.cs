// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 FaDeOkno <143940725+FaDeOkno@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 FaDeOkno <logkedr18@gmail.com>
// SPDX-FileCopyrightText: 2025 coderabbitai[bot] <136622811+coderabbitai[bot]@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Linq;
using Content.Goobstation.Common.Research;
using Content.Shared.Research.Components;
using Content.Shared.Research.Prototypes;
using JetBrains.Annotations;
using Robust.Client.UserInterface;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Client.Research.UI;

[UsedImplicitly]
public sealed class FancyResearchConsoleBoundUserInterface(EntityUid owner, Enum uiKey)
    : BoundUserInterface(owner, uiKey)
{
    [ViewVariables]
    private FancyResearchConsoleMenu? _consoleMenu;

    protected override void Open()
    {
        base.Open();
        _consoleMenu = this.CreateWindow<FancyResearchConsoleMenu>();
        _consoleMenu.SetEntity(Owner);
        _consoleMenu.OnClose += () => _consoleMenu = null;

        _consoleMenu.OnTechnologyCardPressed += id =>
        {
            SendMessage(new ConsoleUnlockTechnologyMessage(id));
        };

        _consoleMenu.OnServerButtonPressed += () =>
        {
            SendMessage(new ConsoleServerSelectionMessage());
        };
    }

    public override void OnProtoReload(PrototypesReloadedEventArgs args)
    {
        base.OnProtoReload(args);

        if (!args.WasModified<TechnologyPrototype>())
            return;

        if (State is not ResearchConsoleBoundInterfaceState rState)
            return;

        _consoleMenu?.UpdatePanels(rState.Researches);
        _consoleMenu?.UpdateInformationPanel(rState.Points);
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is not ResearchConsoleBoundInterfaceState castState || _consoleMenu == null)
            return;
        _consoleMenu.Initialize(castState.PrototypeTypeId, castState.DisciplineTypeId);
        if (!_consoleMenu.List.Keys.Equals(castState.Researches.Keys))
            _consoleMenu.UpdatePanels(castState.Researches);
        if (_consoleMenu.Points != castState.Points)
            _consoleMenu.UpdateInformationPanel(castState.Points);
    }
}
