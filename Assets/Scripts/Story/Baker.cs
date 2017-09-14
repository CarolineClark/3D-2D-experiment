using System.Collections;
using System.Collections.Generic;
using Mgl;


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
    private List<string> playerChoices;
    private string latestResponse;
    private bool canContinue;

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

    private string GetLatestResponse() {
        return latestResponse;
    }

    private void OnGetResponse(string response) {
        latestResponse = response;
    }

    public INPCConversation StartConversation() {
        if (isWifeSick() && isPlayerIgnorant() && isBakerDownstairs()) {
            if (!isPlayerNextToStairs()) {
                return new Conversation1();
            }
        }
        return null;
    }

    private class Conversation1: INPCConversation {
        bool finished;
        int stage;
        string lastResponse;

        public Conversation1() {
            stage = 0;
            finished = false;
            lastResponse = "";
        }

        public NPCStoryMessage GetStory() {
            switch (stage) {
                case 0:
                    List<string> text = new List<string> {".....", "What do you want?"};
                    List<string> choices  = new List<string> { "Bread.", "Nice apron." };
                    stage = 1;
                    return new NPCStoryMessage("scene 0", text, choices);
                case 1:
                    finished = true;
                    if (lastResponse == "Bread.") {
                        return new NPCStoryMessage("scene 1", new List<string> {"You have no money."}, null);
                    } else if (lastResponse == "Nice apron.") {
                        return new NPCStoryMessage("scene 1", new List<string> {"My wife made it."}, null);
                    } else {
                        return new NPCStoryMessage("scene 1", new List<string> {"this response should never get hit."}, null);
                    }
                default:
                    return new NPCStoryMessage("scene 1", new List<string> {"...."}, null);
            }
        }

        public bool IsFinished() {
            return finished;
        }

        public void SetResponse(string response) {
            lastResponse = response;
        }
    }
}