mergeInto(LibraryManager.library, {

    ShowFullscreenAdv: function () {
        ysdk.adv.showFullscreenAdv();
    },

    ShowRewardedVideo: function () {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => { },
                onRewarded: () => unityInstance.SendMessage('Puzzle', 'ForceComplete'),
                onClose: () => unityInstance.SendMessage('Puzzle', 'Unpause'),
                onError: (e) => { }
            }
        });
    },

    RedirectToYandexGames: function () {
        ysdk.dispatchEvent(ysdk.EVENTS.EXIT);
    }
});