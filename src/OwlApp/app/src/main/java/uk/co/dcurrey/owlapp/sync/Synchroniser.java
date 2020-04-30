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
import uk.co.dcurrey.owlapp.database.characterItem.CharacterItemEntity;
import uk.co.dcurrey.owlapp.database.characterSkill.CharacterSkillEntity;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.CharacterSkillRepository;
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
            OwlDatabase.getDb().charItemDao().deleteAll();
            OwlDatabase.getDb().charSkillDao().deleteAll();
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
            parameters.put("IsActive", character.IsActive);
            parameters.put("xp", character.Xp);
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
        String url = APIPaths.getPlayerURL(context)+"api/players";
        HttpsTrustManager.allowAllSSL();

        JSONObject parameters = new JSONObject();
        try
        {
            parameters.put("Id", player.Id);
            parameters.put("FirstName", player.FirstName);
            parameters.put("LastName", player.LastName);
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
                player.IsSynced = true;
                Repository.getInstance().getPlayerRepository().update(player);
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

    public void sendToAPI(Context context, SkillEntity skill)
    {
        String url = APIPaths.getSkillURL(context)+"api/skills";
        HttpsTrustManager.allowAllSSL();

        JSONObject parameters = new JSONObject();
        try
        {
            parameters.put("Id", skill.Id);
            parameters.put("Name", skill.Name);
            parameters.put("Xp", skill.Xp);
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
                skill.IsSynced = true;
                Repository.getInstance().getSkillRepository().update(skill);
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

    public void sendToAPI(Context context, ItemEntity item)
    {
    String url = APIPaths.getSkillURL(context)+"api/craftables";
    HttpsTrustManager.allowAllSSL();

    JSONObject parameters = new JSONObject();
    try
    {
        parameters.put("Id", item.Id);
        parameters.put("Name", item.Name);
        parameters.put("Form", item.Form);
        parameters.put("Requirement", item.Reqs);
        parameters.put("Effect", item.Effect);
        parameters.put("Materials", item.Materials);
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
            item.IsSynced = true;
            Repository.getInstance().getItemRepository().update(item);
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

    public void sendToAPI(Context context, CharacterSkillEntity charSkill)
    {
        String url = APIPaths.getSkillURL(context)+"api/characterskills";
        HttpsTrustManager.allowAllSSL();

        JSONObject parameters = new JSONObject();
        try
        {
            parameters.put("Id", charSkill.Id);
            parameters.put("CharacterId", charSkill.CharacterId);
            parameters.put("SkillId", charSkill.SkillId);
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
                charSkill.IsSynced = true;
                Repository.getInstance().getCharacterSkillRepository().update(charSkill);
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

    public void sendToAPI(Context context, CharacterItemEntity item)
    {
        String url = APIPaths.getItemsURL(context)+"api/bonds";
        HttpsTrustManager.allowAllSSL();

        JSONObject parameters = new JSONObject();
        try
        {
            parameters.put("id", item.Id);
            parameters.put("characterId", item.CharacterId);
            parameters.put("itemId", item.ItemId);
        }
        catch (JSONException e)
        {
            Log.e(this.toString(), "JSON Error", e);
        }

        JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.POST, url, parameters, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response)
            {
                Toast.makeText(context, "POST Success", Toast.LENGTH_SHORT).show();
                item.IsSynced = true;
                Repository.getInstance().getBondRepository().update(item);
            }
        },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error)
                    {
                        Toast.makeText(context, "An Error Occurred", Toast.LENGTH_SHORT).show();
                    }
                });
        VolleySingleton.getInstance(context).addToRequestQueue(jsonObjectRequest);
    }

    public void getFromAPI(Context context)
    {
        // Characters
        getCharactersApi(context);

        // Players
        getPlayersApi(context);

        // Skills
        getSkillsApi(context);

        // Items
        getItemsApi(context);
    }

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
                        character.IsRetired = ((JSONObject) response.get(i)).getBoolean("isRetired");
                        character.PlayerId = ((JSONObject) response.get(i)).getInt("playerId");
                        character.Id = ((JSONObject) response.get(i)).getInt("id");
                        character.Xp = ((JSONObject) response.get(i)).getInt("xp");
                        character.IsActive = ((JSONObject) response.get(i)).getBoolean("isActive");
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
                        skill.Xp = ((JSONObject) response.get(i)).getInt("xpCost");
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

    private void getItemsApi(Context context)
    {
        String url = APIPaths.getItemsURL(context);
        HttpsTrustManager.allowAllSSL();
        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(Request.Method.GET, url+"api/craftables/", null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response)
            {
                for (int i = 0; i < response.length(); i++)
                {
                    ItemEntity item = new ItemEntity();
                    try
                    {
                        item.Id = ((JSONObject) response.get(i)).getInt("id");
                        item.Name = ((JSONObject) response.get(i)).getString("name");
                        item.Effect = ((JSONObject) response.get(i)).getString("effect");
                        item.Form = ((JSONObject) response.get(i)).getString("form");
                        item.Materials = ((JSONObject) response.get(i)).getString("materials");
                        item.Reqs = ((JSONObject) response.get(i)).getString("requirement");
                    } catch (JSONException e)
                    {
                        Log.e(this.toString(), "JSON ERROR", e);
                    }
                    item.IsSynced = true;
                    Repository.getInstance().getItemRepository().insert(item);
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
        syncCharacters(mContext);
        syncPlayers(mContext);
        syncSkills(mContext);
        syncItem(mContext);
        syncCharItem(mContext);
        syncCharSkill(mContext);
    }

    private void syncCharacters(Context mContext)
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
    }

    private void syncCharItem(Context mContext)
    {
        HashMap<Integer, CharacterItemEntity> charItems = Repository.getInstance().getBondRepository().get();

        Iterator it = charItems.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry charItemEntry = (Map.Entry)it.next();
            CharacterItemEntity charItem = (CharacterItemEntity) charItemEntry.getValue();
            if (!charItem.IsSynced){
                sendToAPI(mContext, charItem);
            }
        }
    }

    private void syncItem(Context mContext)
    {
        HashMap<Integer, ItemEntity> items = Repository.getInstance().getItemRepository().get();

        Iterator it = items.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry itemEntry = (Map.Entry)it.next();
            ItemEntity item = (ItemEntity) itemEntry.getValue();
            if (!item.IsSynced){
                sendToAPI(mContext, item);
            }
        }
    }

    private void syncCharSkill(Context mContext)
    {
        HashMap<Integer, CharacterSkillEntity> charSkills = Repository.getInstance().getCharacterSkillRepository().get();

        Iterator it = charSkills.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry charSkillEntry = (Map.Entry)it.next();
            CharacterSkillEntity charSkill = (CharacterSkillEntity) charSkillEntry.getValue();
            if (!charSkill.IsSynced){
                sendToAPI(mContext, charSkill);
            }
        }
    }

    private void syncSkills(Context mContext)
    {
        HashMap<Integer, SkillEntity> skills = Repository.getInstance().getSkillRepository().get();

        Iterator it = skills.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry skillEntry = (Map.Entry)it.next();
            SkillEntity skill = (SkillEntity) skillEntry.getValue();
            if (!skill.IsSynced){
                sendToAPI(mContext, skill);
            }
        }
    }

    private void syncPlayers(Context mContext)
    {
        HashMap<Integer, PlayerEntity> players = Repository.getInstance().getPlayerRepository().get();

        Iterator it = players.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry playEntry = (Map.Entry)it.next();
            PlayerEntity player = (PlayerEntity) playEntry.getValue();
            if (!player.IsSynced){
                sendToAPI(mContext, player);
            }
        }
    }
}
