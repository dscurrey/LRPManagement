package uk.co.dcurrey.owlapp.sync;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;

public class Synchroniser
{
    public static void resetDb()
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
        });
    }

    public static void sendToAPI(Context context, CharacterEntity character)
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

    public static void sendToAPI(Context context, PlayerEntity player)
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

    public static void sendToAPI(Context context, SkillEntity skill)
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
}
