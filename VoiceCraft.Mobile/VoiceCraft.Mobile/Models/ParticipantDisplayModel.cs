﻿using CommunityToolkit.Mvvm.ComponentModel;
using VoiceCraft.Core.Client;

namespace VoiceCraft.Mobile.Models
{
    public partial class ParticipantDisplayModel : ObservableObject
    {
        [ObservableProperty]
        bool isSpeaking;
        [ObservableProperty]
        bool isMuted;
        [ObservableProperty]
        bool isDeafened;
        [ObservableProperty]
        VoiceCraftParticipant participant;

        public ParticipantDisplayModel(VoiceCraftParticipant participant)
        {
            this.participant = participant;
            isMuted = participant.Muted;
            isDeafened = participant.Deafened;
        }
    }
}
