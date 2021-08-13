using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    [SerializeField]private Texture2D[] _texture=new Texture2D[3];
    [Range (2,512)]
    [SerializeField]private int _resolutionLineTexture=128;
    [Range (2,512)]
    [SerializeField]private int _resolutionPointRandTexture=128;
    [Range (2,512)]
    [SerializeField]private int _resolutionGradientTexture=128;
    [Range (2,512)]
    [SerializeField]private int _resolutionRoundTexture=128;
    [SerializeField]private FilterMode _lineTextureFilterMode;
    [SerializeField]private TextureWrapMode _lineTextureWrapMode;
    [SerializeField]private FilterMode _pointRandTextureFilterMode;
    [SerializeField]private TextureWrapMode _pointRandTextureWrapMode;
    [SerializeField]private FilterMode _gradientTextureFilterMode;
    [SerializeField]private TextureWrapMode _gradientTextureWrapMode;
    [SerializeField]private FilterMode _roundTextureFilterMode;
    [SerializeField]private TextureWrapMode _roundTextureWrapMode;
    [SerializeField]private float _radius=0.5f;
    [SerializeField]private Vector2 _offset;
    private Renderer _renderer;
    private Texture2D _bufTexture;
    void Start()
    {
        for(int i=0;i<_texture.Length;i++)
        {
            switch(i)
            {
                case 0:
                if(_texture[i]==null)
                {
                    _texture[i]=CreateTexture(_resolutionLineTexture);
                    GeneratePixels(_texture[i],i);
                }
                if(_texture[i].width!=_resolutionLineTexture)
                {
                    _texture[i].Resize(_resolutionLineTexture,_resolutionLineTexture);
                }
                _texture[i].filterMode=_lineTextureFilterMode;
                _texture[i].wrapMode=_lineTextureWrapMode;
                 break;
                case 1:
                if(_texture[i]==null)
                {
                    _texture[i]=CreateTexture(_resolutionLineTexture);
                    GeneratePixels(_texture[i],i);
                }
                if(_texture[i].width!=_resolutionPointRandTexture)
                {
                    _texture[i].Resize(_resolutionLineTexture,_resolutionPointRandTexture);
                }
                _texture[i].filterMode=_gradientTextureFilterMode;
                _texture[i].wrapMode=_pointRandTextureWrapMode;
                 break;
                case 2:
                if(_texture[i]==null)
                {
                    _texture[i]=CreateTexture(_resolutionGradientTexture);
                    GeneratePixels(_texture[i],i);
                }
                if(_texture[i].width!=_resolutionGradientTexture)
                {
                    _texture[i].Resize(_resolutionGradientTexture,_resolutionGradientTexture);
                }
                _texture[i].filterMode=_gradientTextureFilterMode;
                _texture[i].wrapMode=_gradientTextureWrapMode;
                 break;
                case 3:
                if(_texture[i]==null)
                {
                    _texture[i]=CreateTexture(_resolutionRoundTexture);
                    GeneratePixels(_texture[i],i);
                }
                if(_texture[i].width!=_resolutionRoundTexture)
                {
                    _texture[i].Resize(_resolutionRoundTexture,_resolutionRoundTexture);
                }
                 _texture[i].filterMode=_roundTextureFilterMode;
                 _texture[i].wrapMode=_roundTextureWrapMode;
                 break;
            }
        }
    }
    public Texture2D[] GetTextureArray()
    {
        return _texture;
    }
    public Texture2D GetTexture(int index)
    {
        return _texture[index];
    }
    // Start is called before the first frame update
    /*void OnValidate()
    {
        if(!Application.isPlaying) return;
        
        }
    }*/
    Texture2D CreateTexture(int resolution)
    {
        Texture2D texture=new Texture2D(resolution,resolution);
        return texture;
    }

    void GeneratePixels(Texture2D texture,int index)
    {
        switch(index)
        {
            case 0:
            LineTexture(texture,_resolutionLineTexture);
                return;
            case 1:
            PointRandTexture(texture,_resolutionPointRandTexture);
                return;
            case 2:
            GradientTexture(texture,_resolutionGradientTexture);
                return;
            case 3:
            RoundTexture(texture,_resolutionRoundTexture);
                return;
        }
    }
    void PointRandTexture(Texture2D texture,int resolution)
    {
        for(int y=0;y<resolution;y++)
        {
            for(int x=0;x<resolution;x++)
            {
                float _randomValue=Random.value*0.5f+0.3f;
                texture.SetPixel(x,y,new Color(_randomValue,_randomValue,_randomValue,1));
            }
        }
        texture.Apply();
    }
    void LineTexture(Texture2D texture,int resolution)
    {
        for(int y=0;y<resolution;y++)
        {
            for(int x=0;x<resolution;x++)
            {
                if(x%2==0)
                {
                    texture.SetPixel(x,y,Color.black);
                }
                /*if(y%2!=0)
                {
                    if(x%2!=0)
                    {
                        _texture.SetPixel(x,y,new Color(Random.value, Random.value, Random.value,1f));
                    }
                }
                else{
                    if(x%2==0){
                        _texture.SetPixel(x,y,new Color(Random.value, Random.value, Random.value,1f));
                    }
                }*/
            }
        }
        texture.Apply();
    }
    void GradientTexture(Texture2D texture,int resolution)
    {
        float _step =1f/resolution;
        for(int y=0;y<resolution;y++)
        {
            for(int x=0;x<resolution;x++)
            {
                texture.SetPixel(x,y,new Color(x*_step,y*_step,0f,1f));
            }
        }
        texture.Apply(); 
    }
    void RoundTexture(Texture2D texture,int resolution)
    {
       float _step =1f/resolution;
       for(int y=0;y<resolution;y++)
       {
           for(int x=0;x<resolution;x++)
           {
               float x2=Mathf.Pow((x+0.5f)*_step-_offset.x,2);
               float y2=Mathf.Pow((y+0.5f)*_step-_offset.y,2);
               float r2=Mathf.Pow(_radius,2);
               if(x2+y2<r2)
               {
                   texture.SetPixel(x,y,Color.black);
               }
           }
       } 
       texture.Apply(); 
    }
}
