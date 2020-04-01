package uk.co.dcurrey.owlapp.api.requests;

import android.content.Context;

import androidx.annotation.Nullable;

import com.android.volley.NetworkResponse;
import com.android.volley.Response;
import com.android.volley.toolbox.JsonRequest;

import uk.co.dcurrey.owlapp.api.APIPaths;

public class APIRequest<T> extends JsonRequest<T>
{
    public APIRequest(Context context, int method, String url, @Nullable String requestBody, Response.Listener<T> listener, @Nullable Response.ErrorListener errorListener) {
        super(method, APIPaths.getURL(context) + url, requestBody, listener, errorListener);
    }

    @Override
    protected Response<T> parseNetworkResponse(NetworkResponse response) {
        return null;
    }
}
