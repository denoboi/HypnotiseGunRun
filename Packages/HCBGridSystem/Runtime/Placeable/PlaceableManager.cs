using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class PlaceableManager : Singleton<PlaceableManager>
    {
        private List<IPlaceable> _placeables = new List<IPlaceable>();
        public List<IPlaceable> Placeables { get => _placeables; private set => _placeables = value; }
        
        public void AddPlaceable(IPlaceable placeable) 
        {
            if (Placeables.Contains(placeable))
                return;

            Placeables.Add(placeable);
        }

        public void RemovePlaceable(IPlaceable placeable) 
        {
            if (!Placeables.Contains(placeable))
                return;

            Placeables.Remove(placeable);
        }

        //private void SavePlaceable()
        //{
        //    List<PlaceableSaveData> placeableSaveData = new List<PlaceableSaveData>();
        //    foreach (var product in Placeables)
        //    {
        //        placeableSaveData.Add(new PlaceableSaveData(product.ProductData.ID, product.IsOpened, product.LastPlacedTile.TileID, product.IsMysterious));
        //    }
        //    GridSystemManager.Instance.GridSystemData.PlaceableSaveDatas = placeableSaveData;
        //}

        //private void LoadPlaceable()
        //{
        //    foreach (var productSaveData in GameManager.Instance.PlayerData.ProductSaveDatas)
        //    {
        //        if (!ProductDataManager.Instance.ProductDatasByID.ContainsKey(productSaveData.ProductID))
        //            continue;

        //        ITile tile = TileManager.Instance.GetTile(productSaveData.LastPlacedTileID);
        //        if (tile == null)
        //            continue;

        //        ProductData productData = ProductDataManager.Instance.ProductDatasByID[productSaveData.ProductID];
        //        CreateProduct(productData, tile, productSaveData.IsOpened, productSaveData.IsMysterious);
        //    }

        //    EventManager.OnProductDataLoaded.Invoke();
        //}
    }
}
