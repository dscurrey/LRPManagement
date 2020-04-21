package uk.co.dcurrey.owlapp.sync;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.Network;
import android.net.NetworkCapabilities;
import android.net.NetworkInfo;
import android.net.NetworkRequest;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class NetworkMonitor extends ConnectivityManager.NetworkCallback
{
    private Synchroniser mSync;
    final NetworkRequest networkRequest;
    private Context mContext;

    public NetworkMonitor()
    {
        mSync = new Synchroniser();
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
        if (NetworkMonitor.checkNetConnectivity(mContext))
        {
            HashMap<Integer, CharacterEntity> characters = Repository.getInstance().getCharacterRepository().get();

            Iterator it = characters.entrySet().iterator();
            while (it.hasNext())
            {
                Map.Entry charEntry = (Map.Entry)it.next();
                CharacterEntity character = (CharacterEntity) charEntry.getValue();
                if (!character.IsSynced){
                    mSync.sendToAPI(mContext, character);

                }
            }

            HashMap<Integer, PlayerEntity> players = Repository.getInstance().getPlayerRepository().get();

            it = players.entrySet().iterator();
            while (it.hasNext())
            {
                Map.Entry playEntry = (Map.Entry)it.next();
                PlayerEntity player = (PlayerEntity) playEntry.getValue();
                if (!player.IsSynced){
                    mSync.sendToAPI(mContext, player);
                }
            }

            HashMap<Integer, SkillEntity> skills = Repository.getInstance().getSkillRepository().get();

            it = skills.entrySet().iterator();
            while (it.hasNext())
            {
                Map.Entry skillEntry = (Map.Entry)it.next();
                SkillEntity skill = (SkillEntity) skillEntry.getValue();
                if (!skill.IsSynced){
                    mSync.sendToAPI(mContext, skill);
                }
            }
        }
    }

    public static boolean checkNetConnectivity(Context context)
    {
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = connectivityManager.getActiveNetworkInfo();
        return (networkInfo != null && networkInfo.isConnected());
    }
}
