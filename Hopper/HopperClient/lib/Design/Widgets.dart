import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';

import 'Fonts.dart';

class CustomScaffold extends StatelessWidget {
  final Widget child;
  final double height;
  final double width;
  final bool keyboardFucksUp;
  final bool linear;
  CustomScaffold(
      {this.child,
      this.height,
      this.width,
      this.keyboardFucksUp = false,
      this.linear = true});
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset: keyboardFucksUp,
      backgroundColor: colorBack,
      body: Container(
        padding: EdgeInsets.fromLTRB(18, 18, 18, 0),
        child: child,
        height: height,
        width: width,
        decoration: BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
            stops: [0.1, 0.9],
            colors: [linear ? colorBack2 : colorBack, colorBack],
          ),
        ),
      ),
    );
  }
}

class CustomButton extends StatelessWidget {
  Function onPressed;
  String text;
  CustomButton(this.text, {this.onPressed});
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.all(10),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(5),
        gradient: LinearGradient(
          begin: Alignment.topRight,
          end: Alignment.bottomLeft,
          stops: [0, 1],
          colors: [color1, color2],
        ),
      ),
      child: Material(
        color: Colors.transparent,
        child: InkWell(
            onTap: onPressed,
            child: Container(
              padding: EdgeInsets.symmetric(vertical: 15),
              width: 200,
              height:50,
              child: Text(
                text.toUpperCase(),textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
            )),
      ),
    );
  }
}
class CustomOutlined extends StatelessWidget {
  Function onPressed;
  Widget image;
  String text;
CustomOutlined(this.text,{this.onPressed,this.image});
  @override
  Widget build(BuildContext context) {
    return OutlineButton(
      onPressed: onPressed,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(5)),
      child: Container(
        height:50,
        width:165,
        child: Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            image ?? Container(),
            Padding(
              padding: const EdgeInsets.only(left: 10),
              child: Text(
                text,
                style: TextStyle(
                  fontSize: 20,
                  color: Colors.grey,
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
class CustomTextField extends StatelessWidget {
  String label;
  Function(String) onChanged;
  String initialValue;
  bool obscureText;
  Icon icon;
  CustomTextField(
      {this.label,
      this.onChanged,
      this.initialValue,
      this.obscureText = false,
      this.icon});
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      onChanged: onChanged,
      decoration: InputDecoration(labelText: label, prefixIcon: icon),
      obscureText: obscureText,
      initialValue: initialValue,
    );
  }
}

class CustomError extends StatelessWidget {
  var text;
  CustomError(this.text);
  @override
  Widget build(BuildContext context) {
    return Container(
        child: Text(
      text,
      style: TextStyle(color: Colors.red),
    ));
  }
}

class CustomHeading extends StatelessWidget {
  String text;
  CustomHeading(this.text);
  @override
  Widget build(BuildContext context) {
    return Container(
      margin:EdgeInsets.only(bottom: 20),

      child: Center(
          child: Text(text.toUpperCase(),
              style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 24,
              ))),
    );
  }
}
