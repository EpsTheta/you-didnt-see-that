using System.Data;
using System;
using HarmonyLib;
using System.Collections.Generic;
using Hazel;

namespace TownOfHost
{
    enum CustomRPC
    {
        SyncCustomSettings = 80,
        JesterExiled,
        TerroristWin,
        EndGame,
        PlaySound,
        SetCustomRole,
        SetBountyTarget,
        SetKillOrSpell,
        AddNameColorData,
        RemoveNameColorData,
        ResetNameColorData
    }
    public enum Sounds
    {
        KillSound
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class RPCHandlerPatch
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
        {
            byte packetID = callId;
            switch (packetID)
            {
                case 6: //SetNameRPC
                    string name = reader.ReadString();
                    bool DontShowOnModdedClient = reader.ReadBoolean();
                    Logger.info("名前変更:" + __instance.name + " => " + name); //ログ
                    if (!DontShowOnModdedClient)
                    {
                        __instance.SetName(name);
                    }
                    return false;
            }
            return true;
        }
        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
        {
            byte packetID = callId;
            switch (packetID)
            {
                case (byte)CustomRPC.SyncCustomSettings:
                    foreach (CustomRoles r in Enum.GetValues(typeof(CustomRoles))) r.setCount(reader.ReadInt32());

                    int CurrentGameMode = reader.ReadInt32();
                    bool NoGameEnd = reader.ReadBoolean();
                    bool SwipeCardDisabled = reader.ReadBoolean();
                    bool SubmitScanDisabled = reader.ReadBoolean();
                    bool UnlockSafeDisabled = reader.ReadBoolean();
                    bool UploadDataDisabled = reader.ReadBoolean();
                    bool StartReactorDisabled = reader.ReadBoolean();
                    bool ResetBreakerDisabled = reader.ReadBoolean();
                    int VampireKillDelay = reader.ReadInt32();
                    int SabotageMasterSkillLimit = reader.ReadInt32();
                    bool SabotageMasterFixesDoors = reader.ReadBoolean();
                    bool SabotageMasterFixesReactors = reader.ReadBoolean();
                    bool SabotageMasterFixesOxygens = reader.ReadBoolean();
                    bool SabotageMasterFixesCommunications = reader.ReadBoolean();
                    bool SabotageMasterFixesElectrical = reader.ReadBoolean();
                    int SheriffKillCooldown = reader.ReadInt32();
                    bool SheriffCanKillJester = reader.ReadBoolean();
                    bool SheriffCanKillTerrorist = reader.ReadBoolean();
                    bool SheriffCanKillOpportunist = reader.ReadBoolean();
                    bool SheriffCanKillMadmate = reader.ReadBoolean();
                    bool SyncButtonMode = reader.ReadBoolean();
                    int SyncedButtonCount = reader.ReadInt32();
                    int whenSkipVote = reader.ReadInt32();
                    int whenNonVote = reader.ReadInt32();
                    bool canTerroristSuicideWin = reader.ReadBoolean();
                    bool RandomMapsMode = reader.ReadBoolean();
                    bool AddedTheSkeld = reader.ReadBoolean();
                    bool AddedMiraHQ = reader.ReadBoolean();
                    bool AddedPolus = reader.ReadBoolean();
                    bool AddedTheAirShip = reader.ReadBoolean();
                    bool AllowCloseDoors = reader.ReadBoolean();
                    int HaSKillDelay = reader.ReadInt32();
                    bool IgnoreVent = reader.ReadBoolean();
                    bool IgnoreCosmetics = reader.ReadBoolean();
                    bool MadmateCanFixLightsOut = reader.ReadBoolean();
                    bool MadmateCanFixComms = reader.ReadBoolean();
                    bool MadmateHasImpostorVision = reader.ReadBoolean();
                    int CanMakeMadmateCount = reader.ReadInt32();
                    bool MadGuardianCanSeeBarrier = reader.ReadBoolean();
                    int MadSnitchTasks = reader.ReadInt32();
                    int MayorAdditionalVote = reader.ReadInt32();
                    int SerialKillerCooldown = reader.ReadInt32();
                    int SerialKillerLimit = reader.ReadInt32();
                    int BountyTargetChangeTime = reader.ReadInt32();
                    int BountySuccessKillCooldown = reader.ReadInt32();
                    int BountyFailureKillCooldown = reader.ReadInt32();
                    int BHDefaultKillCooldown = reader.ReadInt32();
                    int ShapeMasterShapeshiftDuration = reader.ReadInt32();
                    RPC.SyncCustomSettings(
                        Options.roleCounts,
                        CurrentGameMode,
                        NoGameEnd,
                        SwipeCardDisabled,
                        SubmitScanDisabled,
                        UnlockSafeDisabled,
                        UploadDataDisabled,
                        StartReactorDisabled,
                        ResetBreakerDisabled,
                        VampireKillDelay,
                        SabotageMasterSkillLimit,
                        SabotageMasterFixesDoors,
                        SabotageMasterFixesReactors,
                        SabotageMasterFixesOxygens,
                        SabotageMasterFixesCommunications,
                        SabotageMasterFixesElectrical,
                        SheriffKillCooldown,
                        SheriffCanKillJester,
                        SheriffCanKillTerrorist,
                        SheriffCanKillOpportunist,
                        SheriffCanKillMadmate,
                        SerialKillerCooldown,
                        SerialKillerLimit,
                        BountyTargetChangeTime,
                        BountySuccessKillCooldown,
                        BountyFailureKillCooldown,
                        BHDefaultKillCooldown,
                        ShapeMasterShapeshiftDuration,
                        SyncButtonMode,
                        SyncedButtonCount,
                        whenSkipVote,
                        whenNonVote,
                        canTerroristSuicideWin,
                        RandomMapsMode,
                        AddedTheSkeld,
                        AddedMiraHQ,
                        AddedPolus,
                        AddedTheAirShip,
                        AllowCloseDoors,
                        HaSKillDelay,
                        IgnoreVent,
                        IgnoreCosmetics,
                        MadmateCanFixLightsOut,
                        MadmateCanFixComms,
                        MadmateHasImpostorVision,
                        CanMakeMadmateCount,
                        MadGuardianCanSeeBarrier,
                        MadSnitchTasks,
                        MayorAdditionalVote
                    );
                    break;
                case (byte)CustomRPC.JesterExiled:
                    byte exiledJester = reader.ReadByte();
                    RPC.JesterExiled(exiledJester);
                    break;
                case (byte)CustomRPC.TerroristWin:
                    byte wonTerrorist = reader.ReadByte();
                    RPC.TerroristWin(wonTerrorist);
                    break;
                case (byte)CustomRPC.EndGame:
                    RPC.EndGame();
                    break;
                case (byte)CustomRPC.PlaySound:
                    byte playerID = reader.ReadByte();
                    Sounds sound = (Sounds)reader.ReadByte();
                    RPC.PlaySound(playerID, sound);
                    break;
                case (byte)CustomRPC.SetCustomRole:
                    byte CustomRoleTargetId = reader.ReadByte();
                    CustomRoles role = (CustomRoles)reader.ReadByte();
                    RPC.SetCustomRole(CustomRoleTargetId, role);
                    break;
                case (byte)CustomRPC.SetBountyTarget:
                    byte HunterId = reader.ReadByte();
                    byte TargetId = reader.ReadByte();
                    var target = Utils.getPlayerById(TargetId);
                    if (target != null) main.BountyTargets[HunterId] = target;
                    break;
                case (byte)CustomRPC.SetKillOrSpell:
                    byte playerId = reader.ReadByte();
                    bool KoS = reader.ReadBoolean();
                    main.KillOrSpell[playerId] = KoS;
                    break;
                case (byte)CustomRPC.AddNameColorData:
                    byte addSeerId = reader.ReadByte();
                    byte addTargetId = reader.ReadByte();
                    string color = reader.ReadString();
                    RPC.AddNameColorData(addSeerId, addTargetId, color);
                    break;
                case (byte)CustomRPC.RemoveNameColorData:
                    byte removeSeerId = reader.ReadByte();
                    byte removeTargetId = reader.ReadByte();
                    RPC.RemoveNameColorData(removeSeerId, removeTargetId);
                    break;
                case (byte)CustomRPC.ResetNameColorData:
                    RPC.ResetNameColorData();
                    break;
            }
        }
    }
    static class RPC
    {
        public static void SyncCustomSettings(
                Dictionary<CustomRoles, int> roleCounts,
                int CurrentGameMode,
                bool NoGameEnd,
                bool SwipeCardDisabled,
                bool SubmitScanDisabled,
                bool UnlockSafeDisabled,
                bool UploadDataDisabled,
                bool StartReactorDisabled,
                bool ResetBreakerDisabled,
                int VampireKillDelay,
                int SabotageMasterSkillLimit,
                bool SabotageMasterFixesDoors,
                bool SabotageMasterFixesReactors,
                bool SabotageMasterFixesOxygens,
                bool SabotageMasterFixesCommunications,
                bool SabotageMasterFixesElectrical,
                int SheriffKillCooldown,
                bool SheriffCanKillJester,
                bool SheriffCanKillTerrorist,
                bool SheriffCanKillOpportunist,
                bool SheriffCanKillMadmate,
                int SerialKillerCooldown,
                int SerialKillerLimit,
                int BountyTargetChangeTime,
                int BountySuccessKillCooldown,
                int BountyFailureKillCooldown,
                int BHDefaultKillCooldown,
                int ShapeMasterShapeshiftDuration,
                bool SyncButtonMode,
                int SyncedButtonCount,
                int whenSkipVote,
                int whenNonVote,
                bool canTerroristSuicideWin,
                bool RandomMapsMode,
                bool AddedTheSkeld,
                bool AddedMiraHQ,
                bool AddedPolus,
                bool AddedTheAirShip,
                bool AllowCloseDoors,
                int HaSKillDelay,
                bool IgnoreVent,
                bool IgnoreCosmetics,
                bool MadmateCanFixLightsOut,
                bool MadmateCanFixComms,
                bool MadmateHasImpostorVision,
                int CanMakeMadmateCount,
                bool MadGuardianCanSeeBarrier,
                int MadSnitchTasks,
                int MayorAdditionalVote
            )
        {
            Options.roleCounts = roleCounts;

            Options.GameMode.UpdateSelection(CurrentGameMode);
            Options.NoGameEnd.UpdateSelection(NoGameEnd);

            Options.DisableSwipeCard.UpdateSelection(SwipeCardDisabled);
            Options.DisableSubmitScan.UpdateSelection(SubmitScanDisabled);
            Options.DisableUnlockSafe.UpdateSelection(UnlockSafeDisabled);
            Options.DisableUploadData.UpdateSelection(UploadDataDisabled);
            
            Options.DisableStartReactor.UpdateSelection(StartReactorDisabled);
            Options.DisableResetBreaker.UpdateSelection(ResetBreakerDisabled);

            main.currentWinner = CustomWinner.Default;
            main.CustomWinTrigger = false;

            main.VisibleTasksCount = true;

            Options.VampireKillDelay = VampireKillDelay;

            Options.SabotageMasterSkillLimit.UpdateSelection(SabotageMasterSkillLimit);
            Options.SabotageMasterFixesDoors.UpdateSelection(SabotageMasterFixesDoors);
            Options.SabotageMasterFixesReactors.UpdateSelection(SabotageMasterFixesReactors);
            Options.SabotageMasterFixesOxygens.UpdateSelection(SabotageMasterFixesOxygens);
            Options.SabotageMasterFixesComms.UpdateSelection(SabotageMasterFixesCommunications);
            Options.SabotageMasterFixesElectrical.UpdateSelection(SabotageMasterFixesElectrical);

            Options.SheriffKillCooldown = SheriffKillCooldown;
            Options.SheriffCanKillJester = SheriffCanKillJester;
            Options.SheriffCanKillTerrorist = SheriffCanKillTerrorist;
            Options.SheriffCanKillOpportunist = SheriffCanKillOpportunist;
            Options.SheriffCanKillMadmate = SheriffCanKillMadmate;

            Options.SerialKillerCooldown.UpdateSelection(SerialKillerCooldown);
            Options.SerialKillerLimit.UpdateSelection(SerialKillerLimit);
            Options.BountyTargetChangeTime.UpdateSelection(BountyTargetChangeTime);
            Options.BountySuccessKillCooldown.UpdateSelection(BountySuccessKillCooldown);
            Options.BountyFailureKillCooldown.UpdateSelection(BountyFailureKillCooldown);
            Options.BHDefaultKillCooldown.UpdateSelection(BHDefaultKillCooldown);
            Options.ShapeMasterShapeshiftDuration = ShapeMasterShapeshiftDuration;

            Options.SyncButtonMode.UpdateSelection(SyncButtonMode);
            Options.SyncedButtonCount.UpdateSelection(SyncedButtonCount);

            Options.whenSkipVote = (VoteMode)whenSkipVote;
            Options.whenNonVote = (VoteMode)whenNonVote;
            Options.canTerroristSuicideWin = canTerroristSuicideWin;
            
            Options.RandomMapsMode.UpdateSelection(RandomMapsMode);
            Options.AddedTheSkeld.UpdateSelection(AddedTheSkeld);
            Options.AddedMiraHQ.UpdateSelection(AddedMiraHQ);
            Options.AddedPolus.UpdateSelection(AddedPolus);
            Options.AddedTheAirShip.UpdateSelection(AddedTheAirShip);

            Options.AllowCloseDoors = AllowCloseDoors;
            Options.HideAndSeekKillDelay = HaSKillDelay;
            Options.IgnoreVent = IgnoreVent;
            Options.IgnoreCosmetics = IgnoreCosmetics;

            Options.MadmateCanFixLightsOut = MadmateCanFixLightsOut;
            Options.MadmateCanFixComms = MadmateCanFixComms;
            Options.MadGuardianCanSeeWhoTriedToKill = MadGuardianCanSeeBarrier;
            Options.MadmateHasImpostorVision = MadmateHasImpostorVision;
            Options.CanMakeMadmateCount = CanMakeMadmateCount;
            Options.MadGuardianCanSeeWhoTriedToKill = MadGuardianCanSeeBarrier;
            Options.MadSnitchTasks = MadSnitchTasks;

            Options.MayorAdditionalVote = MayorAdditionalVote;
        }
        //SyncCustomSettingsRPC Sender
        public static void SyncCustomSettingsRPC()
        {
            if (!AmongUsClient.Instance.AmHost) return;
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, 80, Hazel.SendOption.Reliable, -1);
            foreach (CustomRoles r in Enum.GetValues(typeof(CustomRoles))) writer.Write(r.getCount());

            writer.Write(Options.GameMode.Selection);
            writer.Write(Options.NoGameEnd.GetBool());
            writer.Write(Options.DisableSwipeCard.GetBool());
            writer.Write(Options.DisableSubmitScan.GetBool());
            writer.Write(Options.DisableUnlockSafe.GetBool());
            writer.Write(Options.DisableUploadData.GetBool());
            writer.Write(Options.DisableStartReactor.GetBool());
            writer.Write(Options.DisableResetBreaker.GetBool());
            writer.Write(Options.VampireKillDelay);
            writer.Write(Options.SabotageMasterSkillLimit.GetSelection());
            writer.Write(Options.SabotageMasterFixesDoors.GetBool());
            writer.Write(Options.SabotageMasterFixesReactors.GetBool());
            writer.Write(Options.SabotageMasterFixesOxygens.GetBool());
            writer.Write(Options.SabotageMasterFixesComms.GetBool());
            writer.Write(Options.SabotageMasterFixesElectrical.GetBool());
            writer.Write(Options.SheriffKillCooldown);
            writer.Write(Options.SheriffCanKillJester);
            writer.Write(Options.SheriffCanKillTerrorist);
            writer.Write(Options.SheriffCanKillOpportunist);
            writer.Write(Options.SheriffCanKillMadmate);
            writer.Write(Options.SyncButtonMode.GetBool());
            writer.Write(Options.SyncedButtonCount.GetSelection());
            writer.Write((int)Options.whenSkipVote);
            writer.Write((int)Options.whenNonVote);
            writer.Write(Options.canTerroristSuicideWin);
            writer.Write(Options.RandomMapsMode.GetBool());
            writer.Write(Options.AddedTheSkeld.GetBool());
            writer.Write(Options.AddedMiraHQ.GetBool());
            writer.Write(Options.AddedPolus.GetBool());
            writer.Write(Options.AddedTheAirShip.GetBool());
            writer.Write(Options.AllowCloseDoors);
            writer.Write(Options.HideAndSeekKillDelay);
            writer.Write(Options.IgnoreVent);
            writer.Write(Options.IgnoreCosmetics);
            writer.Write(Options.MadmateCanFixLightsOut);
            writer.Write(Options.MadmateCanFixComms);
            writer.Write(Options.MadmateHasImpostorVision);
            writer.Write(Options.CanMakeMadmateCount);
            writer.Write(Options.MadGuardianCanSeeWhoTriedToKill);
            writer.Write(Options.MadSnitchTasks);
            writer.Write(Options.MayorAdditionalVote);
            writer.Write(Options.SerialKillerCooldown.GetSelection());
            writer.Write(Options.SerialKillerLimit.GetSelection());
            writer.Write(Options.BountyTargetChangeTime.GetSelection());
            writer.Write(Options.BountySuccessKillCooldown.GetSelection());
            writer.Write(Options.BountyFailureKillCooldown.GetSelection());
            writer.Write(Options.BHDefaultKillCooldown.GetSelection());
            writer.Write(Options.ShapeMasterShapeshiftDuration);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
        public static void PlaySoundRPC(byte PlayerID, Sounds sound)
        {
            if (AmongUsClient.Instance.AmHost)
                RPC.PlaySound(PlayerID, sound);
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaySound, Hazel.SendOption.Reliable, -1);
            writer.Write(PlayerID);
            writer.Write((byte)sound);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
        public static void RpcSetRole(PlayerControl targetPlayer, PlayerControl sendTo, RoleTypes role)
        {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(targetPlayer.NetId, (byte)RpcCalls.SetRole, Hazel.SendOption.Reliable, sendTo.getClientId());
            writer.Write((byte)role);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
        public static void ExileAsync(PlayerControl player)
        {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(player.NetId, (byte)RpcCalls.Exiled, Hazel.SendOption.Reliable, -1);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            player.Exiled();
        }
        public static void JesterExiled(byte jesterID)
        {
            main.ExiledJesterID = jesterID;
            main.currentWinner = CustomWinner.Jester;
            PlayerControl Jester = null;
            PlayerControl Imp = null;
            List<PlayerControl> Impostors = new List<PlayerControl>();
            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p.PlayerId == jesterID) Jester = p;
                if (p.Data.Role.IsImpostor)
                {
                    if (Imp == null) Imp = p;
                    Impostors.Add(p);
                }
            }
            if (AmongUsClient.Instance.AmHost)
            {
                foreach (var imp in Impostors)
                {
                    imp.RpcSetRole(RoleTypes.GuardianAngel);
                }
                new LateTask(() => main.CustomWinTrigger = true,
                0.2f, "Custom Win Trigger Task");
            }
        }
        public static void TerroristWin(byte terroristID)
        {
            main.WonTerroristID = terroristID;
            main.currentWinner = CustomWinner.Terrorist;
            PlayerControl Terrorist = null;
            List<PlayerControl> Impostors = new List<PlayerControl>();
            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p.PlayerId == terroristID) Terrorist = p;
                if (p.Data.Role.IsImpostor)
                {
                    Impostors.Add(p);
                }
            }
            if (AmongUsClient.Instance.AmHost)
            {
                foreach (var imp in Impostors)
                {
                    imp.RpcSetRole(RoleTypes.GuardianAngel);
                }
                new LateTask(() => main.CustomWinTrigger = true,
                0.2f, "Custom Win Trigger Task");
            }
        }
        public static void EndGame()
        {
            main.currentWinner = CustomWinner.Draw;
            if (AmongUsClient.Instance.AmHost)
            {
                ShipStatus.Instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.ImpostorByKill, false);
            }
        }
        public static void PlaySound(byte playerID, Sounds sound)
        {
            if (PlayerControl.LocalPlayer.PlayerId == playerID)
            {
                switch (sound)
                {
                    case Sounds.KillSound:
                        SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.8f);
                        break;
                }
            }
        }
        public static void SetCustomRole(byte targetId, CustomRoles role)
        {
            main.AllPlayerCustomRoles[targetId] = role;
            HudManager.Instance.SetHudActive(true);
        }

        public static void AddNameColorData(byte seerId, byte targetId, string color)
        {
            NameColorManager.Instance.Add(seerId, targetId, color);
        }
        public static void RemoveNameColorData(byte seerId, byte targetId)
        {
            NameColorManager.Instance.Remove(seerId, targetId);
        }
        public static void ResetNameColorData()
        {
            NameColorManager.Begin();
        }
    }
}
