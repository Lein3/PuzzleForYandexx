mergeInto(LibraryManager.library, {

    ShowFullscreenAdvExtern: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: () => setTimeout(() => unityInstance.SendMessage('YandexManager', 'AllowShowAdv'), 61000),
                onClose: () => unityInstance.SendMessage('YandexManager', 'UnpauseGame'),
                onError: (e) => unityInstance.SendMessage('YandexManager', 'UnpauseGame'),
            }
        })
    },

    ShowRewardedVideoExtern: function () {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => { },
                onRewarded: () => unityInstance.SendMessage('Puzzle', 'ForceComplete'),
                onClose: () => unityInstance.SendMessage('YandexManager', 'UnpauseGame'),
                onError: (e) => { }
            }
        });
    },

    IsMobileExtern: function () {
        return IsMobile;
    },
});