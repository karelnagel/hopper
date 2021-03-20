import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';
import 'package:hopper/Design/Fonts.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/Pages/AddSoundPage.dart';
import 'package:hopper/main.dart';
import 'package:hopper/models/Sound.dart';

class SoundPage extends StatefulWidget {
  Sound sound;
  SoundPage({Key key, @required this.sound}) : super(key: key);
  @override
  _SoundPageState createState() => _SoundPageState();
}

class _SoundPageState extends State<SoundPage> {
  @override
  Widget build(BuildContext context) {
    var sound = widget.sound;
    return CustomScaffold(
        child: Container(
      margin: EdgeInsets.fromLTRB(60, 60, 60, 0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          CustomHeading("Sound"),
          Text(
            sound.name,
            style: h1,
          ),
          SizedBox(height: 20),
          Text(
            sound.author,
            style: h2,
          ),
          SizedBox(height: 10),
          Text(
            sound.video,
            style: h2,
          ),SizedBox(height: 10),
          Text(
            sound.language,
            style: h2,
          ),
          SizedBox(height: 10),
          Text(
            "Likes: ${sound.likes}",
            style: h2,
          ),
          SizedBox(height: 20),
          Center(
            child: CustomButton(
              "Play",
              onPressed: () {
                sound.play();
              },
            ),
          ),
          Center(
            child: CustomButton(
              "Share",
              onPressed: () {
                sound.share();
              },
            ),
          ),
          sound.creator == applicationUser.id
              ? Center(
                child: CustomButton(
                    'Edit',
                    onPressed: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => AddSoundPage(sound: sound)));
                    },
                  ),
              )
              : Container(),
        ],
      ),
    ));
  }
}
