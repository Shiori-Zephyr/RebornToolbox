using Dalamud.Game.Inventory.InventoryEventArgTypes;
using ECommons.Automation;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using Dalamud.Bindings.ImGui;

namespace RebornToolbox.Features.InvSort;

public class InvSort
{
    public InvSort()
    {
        Svc.GameInventory.InventoryChanged += OnInventoryChanged;
    }

    private DateTime lastSort = DateTime.MinValue;
    private void OnInventoryChanged(IReadOnlyCollection<InventoryEventArgs> events)
    {
        if (!Plugin.Configuration.InvSortConfig.Enabled || (ImGui.IsKeyDown(ImGuiKey.LeftCtrl) || ImGui.IsKeyDown(ImGuiKey.RightCtrl)))
            return;

        if (DateTime.Now > lastSort.AddSeconds(Plugin.Configuration.InvSortConfig.MinUpdateInterval))
        {
            Svc.Log.Verbose($"Sorting inventory");
            lastSort = DateTime.Now;
            Chat.ExecuteCommand("/isort condition inventory ilv des");
            Chat.ExecuteCommand("/isort execute inventory");
            Chat.ExecuteCommand("/isort clear inventory");
        }
    }
}
