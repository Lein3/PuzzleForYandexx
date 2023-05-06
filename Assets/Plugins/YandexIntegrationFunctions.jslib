mergeInto(LibraryManager.library, {

    ShowFullscreenAdv: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: () => unityInstance.SendMessage('Music', 'StartAdvShowed'),
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