package uk.co.dcurrey.owlapp.api.requests;

import android.content.Context;
import android.net.ConnectivityManager;

import androidx.annotation.Nullable;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;

import java.util.HashMap;
import java.util.Map;

public class APIAuthorisationRequest<T> extends APIRequest<T>
{

    private Context mContext;

    public APIAuthorisationRequest(Context context, int method, String url, @Nullable String requestBody, Response.Listener<T> listener, @Nullable Response.ErrorListener errorListener) {
        super(context, method, url, requestBody, listener, errorListener);
        mContext = context;
    }

    // TODO - Implement once auth is created in backend
//    @Override
//    public Map<String, String> getHeaders() throws AuthFailureError
//    {
//        Map<String, String>  params = new HashMap<String, String>();
//        params.put("Authorization", "Bearer " + Repository.getInstance().getUserRepository().getUser().AccessToken);
//        return params;
//    }
}
