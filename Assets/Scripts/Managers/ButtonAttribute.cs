using System;

internal class ButtonAttribute : Attribute
{
     private string v;

     public ButtonAttribute(string v)
     {
          this.v = v;
     }
}