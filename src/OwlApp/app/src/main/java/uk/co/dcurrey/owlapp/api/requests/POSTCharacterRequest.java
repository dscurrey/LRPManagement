package uk.co.dcurrey.owlapp.api.requests;

import android.content.Context;
import android.util.Log;

import com.android.volley.NetworkResponse;
import com.android.volley.ParseError;
import com.android.volley.Response;
import com.android.volley.toolbox.HttpHeaderParser;

import org.json.JSONArray;

import java.util.HashMap;
import java.util.Map;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;

public class POSTCharacterRequest extends APIAuthorisationRequest<JSONArray>
{
    private CharacterEntity characterEntity;

    public POSTCharacterRequest(Context context, CharacterEntity characterEntity)
    {
        super(context, Method.POST, "characters/", null, null, null);
        this.characterEntity = characterEntity;
    }

    @Override
    protected Response parseNetworkResponse(NetworkResponse response)
    {
        if (response.statusCode == 200)
        {
            Log.v("OWL_POSTCharacter","Character POSTed");
            return Response.success(null, HttpHeaderParser.parseCacheHeaders(response));
        }
        else
        {
            Log.v("OWL_POSTCharacter", "Character Didn't POST");
        }
        return Response.error(new ParseError());
    }

    @Override
    protected Map<String, String> getParams()
    {
        Map<String, String> params = new HashMap<String, String>();
        //params.put("Id", ""+characterEntity.Id);
        params.put("Name", characterEntity.Name);
        params.put("IsRetired", ""+characterEntity.IsRetired);
        params.put("PlayerId", ""+characterEntity.PlayerId);
        return params;
    }
}
