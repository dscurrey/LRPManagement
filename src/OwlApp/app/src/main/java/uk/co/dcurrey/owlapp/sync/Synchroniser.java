package uk.co.dcurrey.owlapp.sync;

import android.content.Context;
import android.util.Log;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.api.requests.APIRequest;
import uk.co.dcurrey.owlapp.api.requests.GETCharactersRequest;
import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class Synchroniser
{
    public static void resetDb(Context context)
    {
        // Clear DB
        OwlDatabase.databaseWriteExecutor.execute(() ->
        {
            OwlDatabase.getDb().skillDao().deleteAll();
            OwlDatabase.getDb().characterDao().deleteAll();
            OwlDatabase.getDb().playerDao().deleteAll();
        });

        // Repopulate Db
        // TODO - Implement API Get Requests
        // Dev:
        OwlDatabase.databaseWriteExecutor.execute(() ->
        {
            SkillEntity skill = new SkillEntity();
            skill.Name = "Repop Skill";
            skill.IsSynced = false;
            OwlDatabase.getDb().skillDao().insertAll(skill);

            PlayerEntity player = new PlayerEntity();
            player.FirstName = "Repopulated";
            player.LastName = "Player";
            player.IsSynced = false;
            OwlDatabase.getDb().playerDao().insertAll(player);

            CharacterEntity character = new CharacterEntity();
            character.PlayerId = 42;
            character.IsRetired = true;
            character.IsSynced = false;
            character.Name = "Repop Character";
            OwlDatabase.getDb().characterDao().insertAll(character);

            Synchroniser synchroniser = new Synchroniser();
            synchroniser.getCharactersApi(context);
        });
    }

    public void sendToAPI(Context context, CharacterEntity character)
    {
        Map<String, String> params = new HashMap();
        params.put("Name", character.Name);
        params.put("IsRetired", ""+character.IsRetired);
        params.put("PlayerId", ""+character.PlayerId);

        // TODO - Refactor requests to separate classes

        JSONObject parameters = new JSONObject(params);
        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, APIPaths.getURL(context)+"api/characters", parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                // Success
                String resp = null;
                try
                {
                    resp = response.getString("response");
                } catch (JSONException e)
                {
                    e.printStackTrace();
                }
                if (resp.equals("OK"))
                {
                    character.IsSynced = true;
                    OwlDatabase.getDb(context).characterDao().update(character);
                }
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        //Failure
                    }
                });
        VolleySingleton.getInstance(context).getRequestQueue().add(req);
    }

    public void sendToAPI(Context context, PlayerEntity player)
    {
        Map<String, String> params = new HashMap();
        params.put("FirstName", player.FirstName);
        params.put("LastName", player.LastName);

        // TODO - Refactor requests to separate classes

        JSONObject parameters = new JSONObject(params);
        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, APIPaths.getURL(context)+"api/players", parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                // Success
                String resp = null;
                try
                {
                    resp = response.getString("response");
                } catch (JSONException e)
                {
                    e.printStackTrace();
                }
                if (resp.equals("OK"))
                {
                    player.IsSynced = true;
                    OwlDatabase.getDb(context).playerDao().update(player);
                }
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        //Failure
                    }
                });
        VolleySingleton.getInstance(context).getRequestQueue().add(req);
    }

    public void sendToAPI(Context context, SkillEntity skill)
    {
        Map<String, String> params = new HashMap();
        params.put("Name", skill.Name);

        // TODO - Refactor requests to separate classes

        JSONObject parameters = new JSONObject(params);
        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, APIPaths.getURL(context)+"api/skills", parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                // Success
                String resp = null;
                try
                {
                    resp = response.getString("response");
                } catch (JSONException e)
                {
                    e.printStackTrace();
                }
                if (resp.equals("OK"))
                {
                    skill.IsSynced = true;
                    OwlDatabase.getDb(context).skillDao().update(skill);
                }
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        //Failure
                    }
                });
        VolleySingleton.getInstance(context).getRequestQueue().add(req);
    }

    public void getFromAPI(Context context)
    {
        // Characters
        getCharactersApi(context);

        // Players
        getPlayersApi(context);

        // Skills
        getSkillsApi(context);
    }

    private void getCharactersApi(Context context)
    {
        // Get
        JsonArrayRequest req = new JsonArrayRequest(Request.Method.GET, APIPaths.getURL(context) + "api/characters", null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray resp)
            {
                // Success
                Log.d(this.toString(), "GET CHARACTERS SUCCESS");

                for (int i = 0; i < resp.length(); i++)
                {
                    CharacterEntity characterEntity = new CharacterEntity();
                    try
                    {

                        characterEntity.Id = Integer.valueOf(((JSONObject) resp.get(i)).getString("id"));
                        characterEntity.Name = ((JSONObject) resp.get(i)).getString("name");
                        characterEntity.IsRetired = Boolean.getBoolean(((JSONObject) resp.get(i)).getString("isRetired"));
                        characterEntity.PlayerId = Integer.valueOf(((JSONObject) resp.get(i)).getString("playerId"));
                    } catch (JSONException e)
                    {
                        Log.e(this.toString(), "Error Occured Parsing JSON", e);
                        //e.printStackTrace();
                    }
                    Repository.getInstance().getCharacterRepository().insert(characterEntity);
                }
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        // Fail
                        Log.i(this.toString(), "GET CHARACTERS FAIL");
                    }
                });

        VolleySingleton.getInstance(context).getRequestQueue().add(req);
    }

    private void getPlayersApi(Context context)
    {

    }

    private void getSkillsApi(Context context)
    {

    }

}
