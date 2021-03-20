import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';
import 'package:hopper/Design/Fonts.dart';
import 'package:hopper/Design/Icons.dart';
import 'package:hopper/Pages/AddSoundPage.dart';
import 'package:hopper/Pages/SoundPage.dart';
import 'package:hopper/models/Sound.dart';

class SoundWidget extends StatefulWidget {
  final Sound sound;

  const SoundWidget({Key key, this.sound}) : super(key: key);
  @override
  _SoundWidgetState createState() => _SoundWidgetState();
}

class _SoundWidgetState extends State<SoundWidget> {
  double animationWidth = 0;
  int animationTime = 0;
  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double height = MediaQuery.of(context).size.height;
    var sound = widget.sound;
    return Column(
      children: [
        Container(
            alignment: Alignment.center,
            margin: EdgeInsets.only(bottom: 12),
            width: width,
            child: Row(
              children: [
                InkWell(
                  child: Icon(
                    iconPlay,
                    size: 50,
                    color: colorPrimary,
                  ),
                  onTap: () async {
                    await sound.play();
                  },
                ),
                InkWell(
                  child: Container(
                    width:width-183,
                    margin: EdgeInsets.only(left: 11),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Container(
                          child: Text(
                            sound.name,
                            style: h3,
                            overflow: TextOverflow.clip,maxLines: 1,
                          ),
                        ),
                        SizedBox(
                          height: 10,
                        ),
                        Container(
                            child: Text(
                                sound.video == ''
                                    ? sound.author
                                    : "${sound.author}: ${sound.video}",
                                style: h4,
                              overflow: TextOverflow.clip, maxLines:1,
                            ))
                      ],
                    ),
                  ),
                  onTap: () async {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => SoundPage(sound: sound)));
                  },
                ),
                Spacer(),
                InkWell(
                  child: ActionButton(
                      sound: sound, icon: sound.liked ? iconLiked : iconLike),
                  onTap: () async {
                    sound.liked = !sound.liked;
                    setState(() {});
                    await sound.like();
                  },
                ),
                SizedBox(
                  width: 15,
                ),
                InkWell(
                    child: ActionButton(sound: sound, icon: iconShare),
                    onTap: () async {
                      sound.share();
                    })
              ],
            )),
        Container(
          margin: EdgeInsets.only(bottom: 12),
          child: Divider(
            color: colorWhite,
          ),
        )
      ],
    );
  }
}

class ActionButton extends StatelessWidget {
  const ActionButton({Key key, @required this.sound, this.icon})
      : super(key: key);

  final Sound sound;
  final IconData icon;

  @override
  Widget build(BuildContext context) {
    return Icon(icon, color: colorWhite,size:26);
  }
}
