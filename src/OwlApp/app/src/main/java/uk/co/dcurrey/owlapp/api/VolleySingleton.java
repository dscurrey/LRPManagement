package uk.co.dcurrey.owlapp.api;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;

public class VolleySingleton
{
    private static VolleySingleton mInstance;
    private RequestQueue mRequestQueue;
    private static Context mContext;

    public VolleySingleton(Context context)
    {
        mContext = context;
        mRequestQueue = getRequestQueue();
    }

    public static synchronized VolleySingleton getInstance(Context context)
    {
        if (mInstance == null)
        {
            // Create instance
            mInstance = new VolleySingleton(context);
        }
        return mInstance;
    }

    public static synchronized VolleySingleton getInstance()
    {
        if (mInstance == null)
        {
            throw new IllegalStateException(VolleySingleton.class.getSimpleName()+" is not initialised, call getInstance(context) first.");
        }
        return mInstance;
    }

    public RequestQueue getRequestQueue()
    {
        if (mRequestQueue == null)
        {
            mRequestQueue = Volley.newRequestQueue(mContext.getApplicationContext());
        }
        return mRequestQueue;
    }

    public <T> void addToRequestQueue(Request<T> req)
    {
        getRequestQueue().add(req);
    }
}
