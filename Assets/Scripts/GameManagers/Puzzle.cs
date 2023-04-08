using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameManagers
{
    public class Puzzle : MonoBehaviour
    {
        public RawImage imageToSplit;
        public Vector2 gridSize = new Vector2(2, 2);
        public float delayAfterLevel = 1;
        public Button nextLevelButton;

        internal EventSystem eventSystem;
        internal RawImage imageObject;
        internal Transform imageCurrent;
        internal RectTransform gridObject;
        internal RectTransform cursor;
        internal bool isGameOver = false;
        internal bool isSwapping = false;

        void Start()
        {
            Input.multiTouchEnabled = false;
            eventSystem = EventSystem.current;

            if (GameObject.Find("ImageObject"))
            {
                imageObject = transform.Find("ImageObject").GetComponent<RawImage>();
                imageObject.gameObject.SetActive(false);
            }

            if (GameObject.Find("GridObject"))
            {
                gridObject = transform.Find("GridObject").GetComponent<RectTransform>();
            }

            if (GameObject.Find("Cursor"))
            {
                cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
                cursor.gameObject.SetActive(false);
            }

            StartCoroutine(nameof(UpdateLevel));
        }

        void Update()
        {
            if (cursor)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    cursor.gameObject.SetActive(true);

                if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0 || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                    cursor.gameObject.SetActive(false);

                if (eventSystem.currentSelectedGameObject)
                    cursor.position = eventSystem.currentSelectedGameObject.GetComponent<RectTransform>().position;
            }
        }

        public void SelectImage(Transform source)
        {
            if (isSwapping == false && isGameOver == false)
            {
                if (imageCurrent == null)
                {
                    imageCurrent = source;
                    if (imageCurrent.GetComponent<Animation>())
                        imageCurrent.GetComponent<Animation>().Play("TileSelect");
                }
                else if (imageCurrent == source)
                {
                    if (imageCurrent.GetComponent<Animation>())
                        imageCurrent.GetComponent<Animation>().Play("TileIdle");

                    imageCurrent = null;
                }
                else
                {
                    isSwapping = true;
                    StartCoroutine(SwapImages(imageCurrent, source.position, source, imageCurrent.position));

                    if (imageCurrent.GetComponent<Animation>())
                        imageCurrent.GetComponent<Animation>().Play("TileIdle");
                }

                foreach (Transform imageObject in gridObject)
                    if (imageObject.GetComponent<Button>())
                        imageObject.GetComponent<Button>().enabled = true;
            }
        }

        IEnumerator UpdateLevel()
        {
            imageObject = imageToSplit;
            Vector2 tileSize = new Vector2(imageObject.rectTransform.sizeDelta.x / gridSize.x, imageObject.rectTransform.sizeDelta.y / gridSize.y);
            gridObject.sizeDelta = imageObject.rectTransform.sizeDelta;
            gridObject.position = imageObject.transform.position;
            int tileIndex = 0;
            for (var index = 0; index < gridSize.x; index++)
            {
                for (var indexB = 0; indexB < gridSize.y; indexB++)
                {
                    RawImage imagePart = Instantiate(imageObject) as RawImage;
                    imagePart.uvRect = new Rect(index * (1 / gridSize.x), indexB * (1 / gridSize.y), 1 / gridSize.x, 1 / gridSize.y);
                    imagePart.rectTransform.sizeDelta = gridObject.GetComponent<GridLayoutGroup>().cellSize = tileSize;
                    imagePart.rectTransform.SetParent(gridObject);
                    imagePart.name = tileIndex.ToString();
                    tileIndex++;
                    imagePart.gameObject.SetActive(true);
                }
            }

            foreach (Transform gridTile in gridObject)
            {
                if (Random.value > 0.5f)
                    gridTile.SetAsFirstSibling();
                else
                    gridTile.SetAsLastSibling();
            }
            yield return new WaitForEndOfFrame();

            foreach (Transform gridTile in gridObject)
                gridTile.GetComponent<Button>().enabled = true;

            if (eventSystem && gridObject.Find("00"))
                eventSystem.SetSelectedGameObject(gridObject.Find("00").gameObject);
        }

        IEnumerator SwapImages(Transform firstImage, Vector3 firstTarget, Transform secondImage, Vector3 secondTarget)
        {
            int iterations = 15;
            while (iterations > 0)
            {
                yield return new WaitForFixedUpdate();
                firstImage.position = new Vector3(Mathf.Lerp(firstImage.position.x, firstTarget.x, Time.deltaTime * 10), Mathf.Lerp(firstImage.position.y, firstTarget.y, Time.deltaTime * 10), firstTarget.z);
                secondImage.position = new Vector3(Mathf.Lerp(secondImage.position.x, secondTarget.x, Time.deltaTime * 10), Mathf.Lerp(secondImage.position.y, secondTarget.y, Time.deltaTime * 10), secondTarget.z);
                iterations--;
            }
            iterations = 5;
            while (iterations > 0)
            {
                firstImage.localPosition = new Vector3(secondImage.localPosition.x, secondImage.localPosition.y, Mathf.Lerp(secondImage.localPosition.z, 0, Time.deltaTime * 10));
                secondImage.localPosition = new Vector3(firstImage.localPosition.x, firstImage.localPosition.y, Mathf.Lerp(firstImage.localPosition.z, 0, Time.deltaTime * 10));
                iterations--;
            }
            firstImage.localPosition = new Vector3(secondImage.localPosition.x, secondImage.localPosition.y, 0);
            secondImage.localPosition = new Vector3(firstImage.localPosition.x, firstImage.localPosition.y, 0);
            int tempIndex = secondImage.GetSiblingIndex();
            secondImage.SetSiblingIndex(imageCurrent.GetSiblingIndex());
            imageCurrent.SetSiblingIndex(tempIndex);
            imageCurrent = null;
            isSwapping = false;
            StartCoroutine(CheckImage());
        }

        IEnumerator CheckImage()
        {
            bool correctMatch = true;
            for (var index = 0; index < gridObject.childCount; index++)
            {
                if (gridObject.GetChild(index).name != index.ToString())
                {
                    correctMatch = false;

                    break;
                }
            }

            if (correctMatch == true)
            {
                foreach (Transform imageObject in gridObject)
                    if (imageObject.GetComponent<Button>())
                        imageObject.GetComponent<Button>().enabled = false;

                yield return new WaitForSeconds(delayAfterLevel);

                StartCoroutine(Victory(0.5f));
            }
        }

        public IEnumerator Victory(float delay)
        {
            isGameOver = true;
            nextLevelButton.GetComponent<Animation>().Play("Appear");
            Level.CurrentLevel.Complete();
            Level.CurrentLevel.NextLevel.Unlock();

            yield return new WaitForSeconds(delay);
        }

        public void ForceComplete()
        {
            imageObject.gameObject.SetActive(true);
            gridObject.gameObject.SetActive(false);
            Level.CurrentLevel.Complete();
            Level.CurrentLevel.NextLevel.Unlock();
        }
    }
}