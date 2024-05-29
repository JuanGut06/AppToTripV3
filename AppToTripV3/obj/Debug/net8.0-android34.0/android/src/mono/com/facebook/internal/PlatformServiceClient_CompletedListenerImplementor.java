package mono.com.facebook.internal;


public class PlatformServiceClient_CompletedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.internal.PlatformServiceClient.CompletedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_completed:(Landroid/os/Bundle;)V:GetCompleted_Landroid_os_Bundle_Handler:CrossGeeks.Facebook.Internal.PlatformServiceClient/ICompletedListenerInvoker, CrossGeeks.Facebook.Common.Android\n" +
			"";
		mono.android.Runtime.register ("CrossGeeks.Facebook.Internal.PlatformServiceClient+ICompletedListenerImplementor, CrossGeeks.Facebook.Common.Android", PlatformServiceClient_CompletedListenerImplementor.class, __md_methods);
	}


	public PlatformServiceClient_CompletedListenerImplementor ()
	{
		super ();
		if (getClass () == PlatformServiceClient_CompletedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("CrossGeeks.Facebook.Internal.PlatformServiceClient+ICompletedListenerImplementor, CrossGeeks.Facebook.Common.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public void completed (android.os.Bundle p0)
	{
		n_completed (p0);
	}

	private native void n_completed (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
