using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HCB.Utilities;
using DG.Tweening;

namespace HCB.SkinSystem.Samples
{
    public class SkinCategoryPanel : MonoBehaviour
    {
        private List<SkinItem> skinItems = new List<SkinItem>();

        public Transform Content;

        public void Initialize(GameObject skinItemPrefab, List<SkinItemData> skinItemDatas)
        {
            ClearSkinItems();
            foreach (var item in skinItemDatas)
            {
                GameObject skinItemObject = Instantiate(skinItemPrefab,Content);
                //skinItemObject.transform.localScale = Vector3.one;
                SkinItem skinItem = skinItemObject.GetComponent<SkinItem>();
                skinItem.Initialize(item);
                skinItems.Add(skinItem);
            }
        }

        public void ClearSkinItems()
        {
            foreach (var item in skinItems)
            {
                Destroy(item.gameObject);
            }

            skinItems.Clear();
        }

        public void UnlockRandomAnimation(SkinItemData skinItemData)
        {
            StartCoroutine(UnlockRandomSkinCo(skinItems.Where(x => x.skinItemData == skinItemData).First()));
        }

        private IEnumerator UnlockRandomSkinCo(SkinItem unlockedSkin)
        {
            int cirleCount = 0;
            float waitTime = 0.5f;

            List<SkinItem> items = new List<SkinItem>(skinItems.Where(x=> !SkinManager.Instance.IsSkinUnlocked(x.skinItemData.SkinId)).ToList());
            items.Shuffle();
            int itemIndex = 0;
            while (cirleCount != 10)
            {
                
                SkinItem skinItem = items[itemIndex];
                skinItem.HighlightItem();
                yield return new WaitForSeconds(waitTime);
                skinItem.DehighlitButton();
                cirleCount++;
                waitTime -= 0.05f;
                waitTime = Mathf.Clamp(waitTime, 0.1f, 1);
                itemIndex++;
                if (itemIndex >= items.Count)
                {
                    itemIndex = 0;
                    items.Shuffle();
                }
                yield return null;
            }

            SkinManager.Instance.BuySkin(unlockedSkin.skinItemData);
            unlockedSkin.transform.DOPunchScale(Vector3.one, 1f, 5, 0f);
        }
    }
}