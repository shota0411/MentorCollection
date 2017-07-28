using UnityEngine;

public static class CustomTransform
{
	// uGUIで、セルを並べる時に使う
	// 拡張メソッド。これについての説明はMentorPurchaseViewのスクショの下に記載
	public static Transform SetParentWithReset (this Transform self, Transform parent)
	{
		self.SetParent(parent);
		self.transform.localPosition = Vector3.zero;
		self.transform.localEulerAngles = Vector3.zero;
		self.transform.localScale = Vector3.one;
		return self;
	}
}