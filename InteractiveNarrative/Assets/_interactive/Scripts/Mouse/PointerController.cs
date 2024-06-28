using System.Collections.Generic;
using UnityEngine;

namespace Mouse
{
    public class PointerController
    {
        private static PointerController _instance;
        public static PointerController Instance
        {
            get
            {
                if (_instance == null) //als de instance niet bestaat maakt hij een nieuw script
                {
                    _instance = new PointerController();
                }
                return _instance;
            }
        }
        private Sprite handSprite, cursorSprite;
        private List<Sprite> sprites;
        private Texture2D cursorTexture, handTexture;
        public bool MouseInputEnabled = true;

        public PointerController()
        {
            sprites = new List<Sprite>();
            // Initialization();
        }

        private void Initialization()
        {
            handSprite = GetSpriteByName("MouseHand");
            cursorSprite = GetSpriteByName("MouseCursor");


            if (cursorSprite != null)
            {
                cursorTexture = cursorSprite.texture;

                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Debug.LogWarning("Cursor sprite not found");
            }

            if (handSprite != null)
            {
                handTexture = handSprite.texture;
            }
            else
            {
                Debug.LogWarning("Hand sprite not found");
            }
        }

        public void ChangeMouseToHand()
        {
            if (handTexture != null)
            {
                Cursor.SetCursor(handTexture, Vector2.zero, CursorMode.Auto);
            }
        }
        public void ChangeMouseToCursor()
        {
            if (cursorTexture != null)
            {
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            }
        }

        public void LoadSprites(string folderPath)
        {
            Sprite[] loadedSprites = Resources.LoadAll<Sprite>(folderPath);
            // Debug.Log(loadedSprites.Length + loadedSprites[0].name);
            if (loadedSprites.Length > 0)
            {
                sprites.AddRange(loadedSprites);
                Initialization();
            }
            else
            {
                Debug.LogError("No sprites found in folder: " + folderPath);
            }
        }

        public Sprite GetSpriteByName(string name)
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == name)
                {
                    return sprite;
                }
            }
            Debug.LogError("Sprite not found: " + name);
            return null;
        }


        public void EnableInput(bool enable)
        {
            MouseInputEnabled = enable;
        }
    }
}
