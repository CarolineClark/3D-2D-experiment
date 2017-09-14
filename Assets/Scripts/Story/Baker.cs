using System.Collections;
using System.Collections.Generic;
using Mgl;
using UnityEngine;


class Baker : INPCStory {
    private I18n i18n = I18n.Instance;
    private enum WifeStatus {
        SICK, DEAD, RECOVERED
    }

    private enum PlayerKnowledge {
        NONE, JUST_DISCOVERED, KNOWS
    }

    private enum BakerLocation {
        DOWNSTAIRS, UPSTAIRS
    }

    private enum PlayerLocation {
        NEXT_TO_STAIRS, ANYWHERE_ELSE
    }

    private WifeStatus wifeStatus;
    private PlayerKnowledge playerKnowledge;
    private BakerLocation bakerLocation;
    private PlayerLocation playerLocation;

    public Baker() {
        LoadState();
    }

    void LoadState() {
        // TODO: populate from StoryManager
        wifeStatus = WifeStatus.SICK;
        playerKnowledge = PlayerKnowledge.NONE;

        // TODO: populate from Unity Scene
        bakerLocation = BakerLocation.DOWNSTAIRS;

        // TODO: make argument of function?
        playerLocation = PlayerLocation.ANYWHERE_ELSE;
    }

    private bool isWifeSick() {
        return wifeStatus == WifeStatus.SICK;
    }

    private bool isPlayerIgnorant() {
        return playerKnowledge == PlayerKnowledge.NONE;
    }

    private bool isBakerDownstairs() {
        return bakerLocation == BakerLocation.DOWNSTAIRS;
    }

    private bool isPlayerNextToStairs() {
        return playerLocation == PlayerLocation.NEXT_TO_STAIRS;
    }

    public INPCConversation StartConversation() {
        if (isWifeSick() && isPlayerIgnorant() && isBakerDownstairs()) {
            if (!isPlayerNextToStairs()) {
                string jsonTree = @"{
                    ""text"": [""..."", ""What do you want?""],
                    ""choices"": [
                        {
                            ""selected"": ""Bread"",
                            ""text"": [""You can't afford it.""]
                        },
                        {
                            ""selected"": ""Nice apron"",
                            ""text"": [""My wife made it.""]
                        }
                    ]
                }";
                return new ConversationHelper(jsonTree);;
            }
        }
        return null;
    }
}