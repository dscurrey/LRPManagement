package uk.co.dcurrey.owlapp.api.requests;

import android.content.Context;
import android.util.Log;

import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.toolbox.HttpHeaderParser;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.List;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class GETCharactersRequest extends APIRequest
{
    public GETCharactersRequest(Context context)
    {
        super(context, Method.GET, "api/characters", null, null, null);
    }

    @Override
    protected Response parseNetworkResponse(NetworkResponse resp)
    {
        try
        {
            JSONArray jsonArray = new JSONArray(new String(resp.data, HttpHeaderParser.parseCharset(resp.headers, PROTOCOL_CHARSET)));

            for (int i = 0; i < jsonArray.length(); i++)
            {
                CharacterEntity characterEntity = new CharacterEntity();
                characterEntity.Id = Integer.valueOf(((JSONObject) jsonArray.get(i)).getString("id"));
                characterEntity.Name = ((JSONObject) jsonArray.get(i)).getString("name");
                characterEntity.IsRetired = Boolean.getBoolean(((JSONObject) jsonArray.get(i)).getString("isretired"));
                characterEntity.PlayerId = Integer.valueOf(((JSONObject) jsonArray.get(i)).getString("playerid"));
                Repository.getInstance().getCharacterRepository().insert(characterEntity);
            }
            return Response.success(jsonArray, HttpHeaderParser.parseCacheHeaders(resp));
        } catch (JSONException | UnsupportedEncodingException e)
        {
            Log.e("GETCHARACTERSREQUEST", "An error occured getting Characters from API", e);
            return Response.error(new ParseError(e));
        }
    }
}
