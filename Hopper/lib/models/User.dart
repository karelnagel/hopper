import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_auth/firebase_auth.dart';

class ApplicationUser {
  ApplicationUser(this.id, this.language, this.shareWithLink,
      this.liked, this.created);
  User user;
  String id;
  String language = "English";
  bool shareWithLink = false;
  List<dynamic> liked = [];
  List<dynamic> created = [];

  Map<String, dynamic> toMap() => {
        'liked': liked,
        'created': created,
        'language': language,
        'shareWithLink': shareWithLink,
      };
  ApplicationUser.start(this.user){
    id = user.uid;
  }
  put() async {
    var data = toMap();
    await FirebaseFirestore.instance.collection('users').doc(id).update(data);
  }

  post() async {
    var data = toMap();
    await FirebaseFirestore.instance.collection('users').doc(id).set(data);
  }

  delete() async {
    await FirebaseFirestore.instance.collection('users').doc(id).delete();
  }

  Future<bool> getData() async {
    var result =
        await FirebaseFirestore.instance.collection('users').doc(id).get();
    if (result.exists) {
      var data = result.data();
      language = data['language'];
      shareWithLink = data['shareWithLink'];
      liked = data['liked'];
      created = data['created'];
      return true;
    } else
      return false;
  }

  like(String likedId) async {
    if (liked.contains(likedId))
      liked.remove(likedId);
    else
      liked.add(likedId);
    put();
  }
}
