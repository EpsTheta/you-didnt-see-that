using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using TOHTOR.API;
using TOHTOR.Extensions;
using TOHTOR.GUI.Patches;
using TOHTOR.Managers;
using TOHTOR.Options;
using TOHTOR.Roles.Legacy;
using UnityEngine;
using VentLib.Logging;

namespace TOHTOR.Patches.Network;

[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.FixedUpdate))]
class ShipFixedUpdatePatch
{
    public static void Postfix(ShipStatus __instance)
    {
        //ここより上、全員が実行する
        if (!AmongUsClient.Instance.AmHost) return;

        Game.CurrentGamemode.FixedUpdate();

        //ここより下、ホストのみが実行する
        if (CustomRoleManager.Static.Vampire.IsEnabled() && TOHPlugin.RefixCooldownDelay >= 0)
            TOHPlugin.RefixCooldownDelay -= Time.fixedDeltaTime;

        else if (!float.IsNaN(TOHPlugin.RefixCooldownDelay))
        {
            TOHPlugin.RefixCooldownDelay = float.NaN;
            VentLogger.Old("Refix Cooldown", "CoolDown");
        }
    }
}
[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.RepairSystem))]
class RepairSystemPatch
{
    public static bool IsComms;
    public static bool Prefix(ShipStatus __instance,
        [HarmonyArgument(0)] SystemTypes systemType,
        [HarmonyArgument(1)] PlayerControl player,
        [HarmonyArgument(2)] byte amount)
    {
        VentLogger.Info("SystemType: " + systemType.ToString() + ", PlayerName: " + player.GetNameWithRole() + ", amount: " + amount, "RepairSystem");
        IsComms = false;
        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
            if (task.TaskType == TaskTypes.FixComms) IsComms = true;

        if (!AmongUsClient.Instance.AmHost) return true; //以下、ホストのみ実行
        //SabotageMaster
        /*if (player.Is(CustomRoles.SabotageMaster))
            SabotageMasterOLD.RepairSystem(__instance, systemType, amount);*/

        // TODO: TLC
        //StaticOptions.DisableAirshipViewingDeckLightsPanel
        //StaticOptions.DisableAirshipGapRoomLightsPanel
        //StaticOptions.DisableAirshipCargoLightsPanel
        /*if (systemType == SystemTypes.Electrical && 0 <= amount && amount <= 4)
        {
            if (!StaticOptions.MadmateCanFixLightsOut && player.GetCustomRole().IsMadmate()) return false; //Madmateが停電を直せる設定がオフ
            switch (TOHPlugin.NormalOptions.MapId)
            {
                case 4:
                    if (StaticOptions.DisableAirshipViewingDeckLightsPanel && Vector2.Distance(player.transform.position, new(-12.93f, -11.28f)) <= 2f) return false;
                    if (StaticOptions.DisableAirshipGapRoomLightsPanel && Vector2.Distance(player.transform.position, new(13.92f, 6.43f)) <= 2f) return false;
                    if (StaticOptions.DisableAirshipCargoLightsPanel && Vector2.Distance(player.transform.position, new(30.56f, 2.12f)) <= 2f) return false;
                    break;
            }
        }*/

        return true;
    }
    public static void CheckAndOpenDoorsRange(ShipStatus __instance, int amount, int min, int max)
    {
        var Ids = new List<int>();
        for (var i = min; i <= max; i++)
        {
            Ids.Add(i);
        }
        CheckAndOpenDoors(__instance, amount, Ids.ToArray());
    }
    private static void CheckAndOpenDoors(ShipStatus __instance, int amount, params int[] DoorIds)
    {
        if (DoorIds.Contains(amount)) foreach (var id in DoorIds)
        {
            __instance.RpcRepairSystem(SystemTypes.Doors, id);
        }
    }
}
/*[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CloseDoorsOfType))]
class CloseDoorsPatch
{
    public static bool Prefix(ShipStatus __instance)
    {
        return !(OldOptions.CurrentGameMode == CustomGameMode.HideAndSeek || StaticOptions.IsStandardHAS) || StaticOptions.AllowCloseDoors;
    }
}*/

[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
class StartPatch
{
    public static void Postfix()
    {
        VentLogger.Old("-----------Start Game-----------", "Phase");
    }
}
[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.StartMeeting))]
class StartMeetingPatch
{
    public static void Prefix(ShipStatus __instance, PlayerControl reporter, GameData.PlayerInfo target)
    {
        Object.FindObjectsOfType<DeadBody>();
    }
}

[HarmonyPatch(typeof(GameManager), nameof(GameManager.CheckTaskCompletion))]
class CheckTaskCompletionPatch
{
    public static bool Prefix(ref bool __result)
    {
        if (!GeneralOptions.GameplayOptions.DisableTaskWin && !GeneralOptions.DebugOptions.NoGameEnd) return true;

        __result = false;

        return false;
    }
}