using AboneTakip.Core.DataAccess;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DataAccess.EntitiyFramework.Context;
using AboneTakip.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DataAccess.EntitiyFramework.Concrete
{
    public class VolumetricRepository : BaseRepository<Volumetric, AboneTakipDbContext>, IVolumetricRepository
    {
        public VolumetricRepository(AboneTakipDbContext context) : base(context) { }
    }
}
