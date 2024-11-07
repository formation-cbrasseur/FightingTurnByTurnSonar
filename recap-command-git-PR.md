# Récap' pour git et faire une PR propre
- On créer une nouvelle branche et on se place dessus
git switch -C feature/Nom-branche
- On fait nos modifs en local, puis on add et commit
git add .
git commit -m "Message du commit"
- On push sur la branche distante (avec l'option -u si c'est une nouvelle branche qui n'existe pas déjà en distant)
git push -u origin feature/Nom-branche
- Quand ça c'est fait, ça propose une PR (soit depuis le terminal, soit depuis l'interface de github)
- Dans cette PR on ajoute un reviewer (On peut aussi Closes #N°issue)
- On valide et ça va passer les workflows d'intégration continue
- Quand et si tout est ok, la PR va permettre de merger la nouvelle version sur main de manière popre et vérifiée
- Maintenant il reste plus qu'à clean les branches inutiles (Déjà, après la PR, github propose de supprimer la branche, faites-le)
- On retourne en local, on fait une synchronisation de nos éléments avec le répo distant
git remote update
- On se place sur main si besoin
git switch main
- On supprime la branche locale
git branch -d feature/Nom-branche
- On supprime la branche distante grâce à la synchro des branches entre le local et le distant
git remote prune origin
- Et c'est terminé, on a géré complètement le workflow d'utilisation de git avec une CI propre