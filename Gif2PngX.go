package main

import (
  "regexp"
  "fmt"
  "log"
  "os"
  "image"
  "image/gif"
  "image/png"
  "image/draw"
)

func main() {
  if len(os.Args)>1{
    fn:=regexp.MustCompile(".gif$")
    for _,v := range os.Args[1:]{
      f,e:=os.Open(v)
      iserr(e)
      log.Println("[  Read GIF  ] File=",f.Name())
      gif,e:=gif.DecodeAll(f)
      f.Close()
      iserr(e)
      log.Println("[ Create PNG ] Size=",len(gif.Image)*150,150)
      dst:=image.NewRGBA(image.Rect(0,0,len(gif.Image)*150,150))
      iserr(e)
      log.Println("[ Slice GIF  ] Size=",len(gif.Image))
      for index,src:=range gif.Image{
        b:=src.Bounds()
        m,n:= 150/2-b.Dx()/2+index*150, 150/2-b.Dy()/2
        r:=image.Rect(m,n,m+b.Dx(),n+b.Dy())
        log.Println("GIF drawto PNG for",index)
        draw.Draw(dst,r,src,image.ZP, draw.Src)
      }
      log.Println("[ Write PNG  ]")
      out,e:=os.Create(fmt.Sprintf("%s.png",fn.ReplaceAllLiteralString(f.Name(),"")))
      iserr(e)
      e=png.Encode(out,dst)
      iserr(e)
      out.Close()
    }
    log.Println("Convert Success. Press any key to exit.")
    var s string
    fmt.Scanf("%s",&s)
  }else {
    fmt.Println("Usage:",os.Args[0],"<filename>")
  }
}

func iserr(e error) {
  if e!=nil {
    log.Fatal(e)
    log.Println("Press any key to exit.")
    var s string
    fmt.Scanf("%s",&s)
  }
}