using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using TMPro;
using UnityEngine.UI;

namespace Game.UI
{
	public class UIFooterPopup : NoodUI 
	{
        [SerializeField] private TextMeshProUGUI _notifyText;
        [SerializeField] private Image _notifyImage;

		public static UIFooterPopup Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIFooterPopup>("Prefab/Game/UI/UIFooterPopup"), parent);
		}

		public void SetText(string text)
		{
            _notifyText.text = text;
        }

        public override void Open()
		{
            StartCoroutine(Appear(_notifyImage));
            StartCoroutine(AppearText(_notifyText));
        }
        public override void Close()
        {
            StartCoroutine(Disappear(_notifyImage));
            StartCoroutine(DisappearText(_notifyText));
        }

		IEnumerator Disappear(Image image)
        {
            while(image.color.a > 0.6f)
            {
                yield return null;
                Color color = image.color;
                color.a -= Time.deltaTime;
                image.color = color;
            }
        }
        IEnumerator DisappearText(TextMeshProUGUI textMesh)
        {
            while(textMesh.color.a > 0)
            {
                yield return null;
                Color color = textMesh.color;
                color.a -= Time.deltaTime;
                textMesh.color = color;
            }
        }

        IEnumerator Appear(Image image)
        {
            while(image.color.a < 1)
            {
                yield return null;
                Color color = image.color;
                color.a += Time.deltaTime;
                image.color = color;
            }
        }
        
        IEnumerator AppearText(TextMeshProUGUI textMesh)
        {
            while(textMesh.color.a < 1)
            {
                yield return null;
                Color color = textMesh.color;
                color.a += Time.deltaTime;
                textMesh.color = color;
            }
        }
	}
}
