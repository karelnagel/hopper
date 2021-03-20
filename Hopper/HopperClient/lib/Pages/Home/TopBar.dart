import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';
import 'package:hopper/Design/Fonts.dart';
import 'package:hopper/Design/Icons.dart';
import 'package:hopper/Pages/AddSoundPage.dart';
import 'package:hopper/Pages/ProfilePage.dart';
import 'package:hopper/main.dart';
import 'package:hopper/models/Sound.dart';

class TopBar extends StatefulWidget {
  final Function setState;

  const TopBar({Key key, this.setState}) : super(key: key);

  @override
  _TopBarState createState() => _TopBarState();
}

class _TopBarState extends State<TopBar> {
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(top: 26),
      child: Row(
        children: [
          InkWell(
              child: Row(
                children: [
                  ProfilePicture(),
                  Column(children: [
                    Text(
                        applicationUser.user.displayName != null &&
                                applicationUser.user.displayName.isNotEmpty
                            ? "Hi ${applicationUser.user.displayName}!"
                            : "Hi!",
                        style: h1),
                    SizedBox(
                      height: 8,
                    ),
                    Text("Welcome back!", style: h2)
                  ], crossAxisAlignment: CrossAxisAlignment.start),
                ],
              ),
              onTap: () => Navigator.push(context,
                  MaterialPageRoute(builder: (context) => ProfilePage()))),
          Spacer(),
          InkWell(
            child: MenuButton(icon: iconAddSound),
            onTap: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => AddSoundPage(
                            sound: Sound.create(null),
                            isNew: true,
                          )));
            },
          ),
        ],
      ),
    );
  }
}

class MenuButton extends StatelessWidget {
  final IconData icon;

  const MenuButton({Key key, this.icon}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      child: Icon(icon, size: 24, color: colorWhite),
      width: 39,
      height: 39,
      decoration: BoxDecoration(
        color: Color.fromRGBO(255, 255, 255, 0.15),
        borderRadius: BorderRadius.circular(10),
      ),
    );
  }
}

class ProfilePicture extends StatelessWidget {
  const ProfilePicture({
    Key key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(right: 15),
      child: ClipOval(
        child: Image.network(
          applicationUser.user.photoURL != null &&
                  applicationUser.user.photoURL.isNotEmpty
              ? applicationUser.user.photoURL
              : "https://picsum.photos/200",
          height: 52,
          width: 52,
          fit: BoxFit.cover,
        ),
      ),
    );
  }
}
