package uk.co.dcurrey.owlapp.sync;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.Network;
import android.net.NetworkCapabilities;
import android.net.NetworkInfo;
import android.net.NetworkRequest;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelStore;
import androidx.lifecycle.ViewModelStoreOwner;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class NetworkMonitor extends ConnectivityManager.NetworkCallback
{
    final NetworkRequest networkRequest;
    private Context mContext;

    public NetworkMonitor()
    {
        networkRequest = new NetworkRequest.Builder()
                .addTransportType(NetworkCapabilities.TRANSPORT_CELLULAR)
                .addTransportType(NetworkCapabilities.TRANSPORT_WIFI)
                .build();
    }

    public void enable(Context context)
    {
        mContext = context;
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        connectivityManager.registerNetworkCallback(networkRequest, this);
    }

    @Override
    public void onAvailable(Network network)
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

//    public boolean checkNetConnectivity(Context context)
//    {
//        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
//        NetworkInfo networkInfo = connectivityManager.getActiveNetworkInfo();
//        return (networkInfo != null && networkInfo.isConnected());
//    }

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

    // TODO - refactor
    public void resetDb()
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
}
