package uk.co.dcurrey.owlapp.sync;

import android.content.Context;
import android.util.Log;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.HttpsTrustManager;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
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
        Synchroniser synchroniser = new Synchroniser();
        synchroniser.getFromAPI(context);
    }

    public void sendToAPI(Context context, CharacterEntity character)
    {
        String url = APIPaths.getCharURL(context)+"api/characters";
        HttpsTrustManager.allowAllSSL();

        JSONObject parameters = new JSONObject();
        try
        {
            parameters.put("Id", character.Id);
            parameters.put("PlayerId", character.PlayerId);
            parameters.put("Name", character.Name);
            parameters.put("IsRetired", character.IsRetired);
        }
        catch (JSONException e)
        {
            Log.e(this.toString(), "JSON ERROR", e);
        }

        JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.POST, url, parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                Toast.makeText(context, "POST SUCCESS", Toast.LENGTH_SHORT).show();
                character.IsSynced = true;
                Repository.getInstance().getCharacterRepository().update(character);
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        Toast.makeText(context, "An error occurred", Toast.LENGTH_SHORT).show();
                    }
                });
        VolleySingleton.getInstance(context).addToRequestQueue(jsonObjectRequest);
    }

    public void sendToAPI(Context context, PlayerEntity player)
    {
    }

    public void sendToAPI(Context context, SkillEntity skill)
    {
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

    // TODO - Refactor Request code (see uk.co.dcurrey.owlapp.api.requests)
    private void getCharactersApi(Context context)
    {
        String url = APIPaths.getCharURL(context);
        HttpsTrustManager.allowAllSSL();
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url+"api/characters/", null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response)
            {
                for (int i = 0; i < response.length(); i++)
                {
                    CharacterEntity character = new CharacterEntity();
                    try
                    {
                        character.Name = ((JSONObject) response.get(i)).getString("name");
                        character.IsRetired = ((JSONObject) response.get(i)).getBoolean("IsRetired");
                        character.PlayerId = ((JSONObject) response.get(i)).getInt("PlayerId");
                        character.Id = ((JSONObject) response.get(i)).getInt("id");
                    } catch (JSONException e)
                    {
                        Log.e(this.toString(), "JSON ERROR", e);
                    }
                    character.IsSynced = true;
                    Repository.getInstance().getCharacterRepository().insert(character);
                }
            }
        }, new Response.ErrorListener()
        {
            @Override
            public void onErrorResponse(VolleyError error)
            {
                Toast.makeText(context, "GET ERROR OCCURRED", Toast.LENGTH_SHORT).show();
            }
        });

        VolleySingleton.getInstance(context).addToRequestQueue(jsonArrayRequest);
    }

    private void getPlayersApi(Context context)
    {
        String url = APIPaths.getPlayerURL(context);
        HttpsTrustManager.allowAllSSL();
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url+"api/players/", null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response)
            {
                for (int i = 0; i < response.length(); i++)
                {
                    PlayerEntity player = new PlayerEntity();
                    try
                    {
                        player.LastName = ((JSONObject) response.get(i)).getString("lastName");
                        player.FirstName = ((JSONObject) response.get(i)).getString("firstName");
                        player.Id = ((JSONObject) response.get(i)).getInt("id");
                    } catch (JSONException e)
                    {
                        Log.e(this.toString(), "JSON ERROR", e);
                    }
                    player.IsSynced = true;
                    Repository.getInstance().getPlayerRepository().insert(player);
                }
            }
        }, new Response.ErrorListener()
        {
            @Override
            public void onErrorResponse(VolleyError error)
            {
                Toast.makeText(context, "GET ERROR OCCURRED", Toast.LENGTH_SHORT).show();
            }
        });

        VolleySingleton.getInstance(context).addToRequestQueue(jsonArrayRequest);
    }

    private void getSkillsApi(Context context)
    {
        String url = APIPaths.getSkillURL(context);
        HttpsTrustManager.allowAllSSL();
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url+"api/skills/", null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response)
            {
                for (int i = 0; i < response.length(); i++)
                {
                    SkillEntity skill = new SkillEntity();
                    try
                    {
                        skill.Id = ((JSONObject) response.get(i)).getInt("id");
                        skill.Name = ((JSONObject) response.get(i)).getString("name");
                    } catch (JSONException e)
                    {
                        Log.e(this.toString(), "JSON ERROR", e);
                    }
                    skill.IsSynced = true;
                    Repository.getInstance().getSkillRepository().insert(skill);
                }
            }
        }, new Response.ErrorListener()
        {
            @Override
            public void onErrorResponse(VolleyError error)
            {
                Toast.makeText(context, "GET ERROR OCCURRED", Toast.LENGTH_SHORT).show();
            }
        });

        VolleySingleton.getInstance(context).addToRequestQueue(jsonArrayRequest);
    }

    public void SyncDbToAPI(Context mContext)
    {
        HashMap<Integer, CharacterEntity> characters = Repository.getInstance().getCharacterRepository().get();

        Iterator it = characters.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry charEntry = (Map.Entry)it.next();
            CharacterEntity character = (CharacterEntity) charEntry.getValue();
            if (!character.IsSynced){
                sendToAPI(mContext, character);

            }
        }

        HashMap<Integer, PlayerEntity> players = Repository.getInstance().getPlayerRepository().get();

        it = players.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry playEntry = (Map.Entry)it.next();
            PlayerEntity player = (PlayerEntity) playEntry.getValue();
            if (!player.IsSynced){
                sendToAPI(mContext, player);
            }
        }

        HashMap<Integer, SkillEntity> skills = Repository.getInstance().getSkillRepository().get();

        it = skills.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry skillEntry = (Map.Entry)it.next();
            SkillEntity skill = (SkillEntity) skillEntry.getValue();
            if (!skill.IsSynced){
                sendToAPI(mContext, skill);
            }
        }
    }

}
