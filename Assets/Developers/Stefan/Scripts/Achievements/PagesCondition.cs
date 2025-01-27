using System;
using UnityEngine;

[Serializable]
public class PagesCondition : BookCondition
{
    [SerializeField] int _requiredNum;

    public override bool CanApply(OwnedBook data)
    {
        return data.CurrentPage >= _requiredNum;
    }
}
