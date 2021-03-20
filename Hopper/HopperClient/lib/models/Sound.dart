import 'dart:io';
import 'dart:typed_data';

import 'package:audioplayers/audioplayers.dart';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:hopper/main.dart';
import 'package:hopper/models/User.dart';
import 'package:uuid/uuid.dart';
import 'package:esys_flutter_share/esys_flutter_share.dart';

AudioPlayer audioPlayer = AudioPlayer();

class Sound {
  String id;
  String name = "";
  String author = "";
  String video = "";
  String creator = "";
  String language = 'English';
  String url = "";
  int likes = 0;
  bool liked = false;

  Sound(this.id, this.name, this.author, this.video, this.url, this.likes,this.language);

  Sound.create(this.id) {
    if (id == null) {
      var uuid = Uuid();
      this.id = uuid.v1();
    }
  }

  Map<String, dynamic> toMap() => {
        'id': id,
        'name': name,
        'author': author,
        'video': video,
        'url': url,
        'likes': likes,
    'creator': creator,
    'language': language,
      };
  factory Sound.get(Map<String, dynamic> data) {
    return Sound(data['id'], data['name'], data['author'], data['video'],
        data['url'], data['likes'],data['language']);
  }
  put() async {
    var data = this.toMap();
    await FirebaseFirestore.instance.collection('sounds').doc(id).update(data);
  }

  post() async {
    var data = this.toMap();
    await FirebaseFirestore.instance.collection('sounds').doc(id).set(data);
  }

  delete() async {
    await FirebaseFirestore.instance.collection('sounds').doc(id).delete();
  }

  get() async {
    var result =
        await FirebaseFirestore.instance.collection('sounds').doc(id).get();
    var data = result.data();
    name = data['name'];
    author = data['author'];
    video = data['video'];
    url = data['url'];
    likes = data['likes'];
    creator = data['creator'];
  }

  Future<Uint8List> getData() async {
    try {
      var data = await FirebaseStorage.instance
          .ref()
          .child("sounds/$id.mp3")
          .getData(10000000);
      return data;
    } catch (e) {
      print(e);
      return null;
    }
  }

  Future<String> getUrl() async {
    try {
      var data = await FirebaseStorage.instance
          .ref()
          .child("sounds/$id.mp3")
          .getDownloadURL();
      return data;
    } catch (e) {
      print(e);
      return null;
    }
  }

  share() async {
    if (applicationUser.shareWithLink) {
      if (url != null) Share.text(name, url, "text/plain");
    } else {
      var data = await getData();
      if (data != null) await Share.file(name, "$name.mp3", data, "audio/mpeg");
    }
  }

  Future<int> play() async {
    if (url != null) {
      audioPlayer.stop();
      audioPlayer.play(url);
      return 1;
    }
    return null;
  }

  like() async {
    await get();
    applicationUser.like(id);
    if (liked) {
      liked = true;
      likes += 1;
    } else {
      liked = false;
      likes -= 1;
    }
    await put();
  }

  uploadFile(File file) async {
    var data = await FirebaseStorage.instance
        .ref()
        .child("sounds/$id.mp3")
        .putFile(file);
  }

  deleteFile() async {
    var data =
        await FirebaseStorage.instance.ref().child("sounds/$id.mp3").delete();
  }
}
