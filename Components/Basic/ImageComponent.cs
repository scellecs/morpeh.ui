namespace Morpeh
{
    using UnityEngine.UI;

    [System.Serializable]
    public struct ImageComponent : IMonoComponent<Image>
    {
        public Image Image;

        public Image monoComponent
        {
            get { return this.Image; }
            set { this.Image = value; }
        }
    }
}
