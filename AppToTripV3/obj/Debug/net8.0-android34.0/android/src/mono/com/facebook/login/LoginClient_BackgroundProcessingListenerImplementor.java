package mono.com.facebook.login;


public class LoginClient_BackgroundProcessingListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.login.LoginClient.BackgroundProcessingListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBackgroundProcessingStarted:()V:GetOnBackgroundProcessingStartedHandler:CrossGeeks.Facebook.Login.LoginClient/IBackgroundProcessingListenerInvoker, CrossGeeks.Facebook.Common.Android\n" +
			"n_onBackgroundProcessingStopped:()V:GetOnBackgroundProcessingStoppedHandler:CrossGeeks.Facebook.Login.LoginClient/IBackgroundProcessingListenerInvoker, CrossGeeks.Facebook.Common.Android\n" +
			"";
		mono.android.Runtime.register ("CrossGeeks.Facebook.Login.LoginClient+IBackgroundProcessingListenerImplementor, CrossGeeks.Facebook.Common.Android", LoginClient_BackgroundProcessingListenerImplementor.class, __md_methods);
	}


	public LoginClient_BackgroundProcessingListenerImplementor ()
	{
		super ();
		if (getClass () == LoginClient_BackgroundProcessingListenerImplementor.class) {
			mono.android.TypeManager.Activate ("CrossGeeks.Facebook.Login.LoginClient+IBackgroundProcessingListenerImplementor, CrossGeeks.Facebook.Common.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public void onBackgroundProcessingStarted ()
	{
		n_onBackgroundProcessingStarted ();
	}

	private native void n_onBackgroundProcessingStarted ();


	public void onBackgroundProcessingStopped ()
	{
		n_onBackgroundProcessingStopped ();
	}

	private native void n_onBackgroundProcessingStopped ();

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
