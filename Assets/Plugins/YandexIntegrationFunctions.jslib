mergeInto(LibraryManager.library, {

    ShowFullscreenAdv: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: () => unityInstance.SendMessage('YandexManager', 'UnpauseGame'),
            }
        })
    },

    ShowRewardedVideo: function () {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => { },
                onRewarded: () => unityInstance.SendMessage('Puzzle', 'ForceComplete'),
                onClose: () => unityInstance.SendMessage('YandexManager', 'UnpauseGame'),
                onError: (e) => { }
            }
        });
    },
});