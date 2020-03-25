namespace Morpeh
{
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct InputFieldComponent : IMonoComponent<InputField>
    {
        public InputField InputField;

        public InputField monoComponent
        {
            get { return this.InputField; }
            set { this.InputField = value; }
        }
    }
}
